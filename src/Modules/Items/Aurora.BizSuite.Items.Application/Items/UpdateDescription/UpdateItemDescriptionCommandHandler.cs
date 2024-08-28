namespace Aurora.BizSuite.Items.Application.Items.UpdateDescription;

internal sealed class UpdateItemDescriptionCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<UpdateItemDescriptionCommand>
{
    public async Task<Result> Handle(
        UpdateItemDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Update item description
        var result = item.UpdateDescription(
            request.ItemDescriptionId,
            request.Description);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}