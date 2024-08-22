namespace Aurora.BizSuite.Items.Application.Items.Update;

internal sealed class UpdateItemCommandHandler(
    IItemRepository itemRepository,
    IBrandRepository brandRepository)
    : ICommandHandler<UpdateItemCommand>
{
    public async Task<Result> Handle(
        UpdateItemCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Get the brand
        var brand = await brandRepository.GetByIdAsync(new BrandId(request.BrandId));
        if (brand is null)
        {
            return Result.Fail(BrandErrors.NotFound(request.BrandId));
        }

        // Update item
        var result = item.Update(
            request.Name,
            request.Description,
            brand,
            request.AlternativeCode,
            request.Notes,
            request.Tags);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(item);

        return Result.Ok();
    }
}