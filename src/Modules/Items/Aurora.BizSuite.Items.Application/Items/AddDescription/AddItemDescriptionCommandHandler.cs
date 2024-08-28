namespace Aurora.BizSuite.Items.Application.Items.AddDescription;

internal sealed class AddItemDescriptionCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<AddItemDescriptionCommand>
{
    public async Task<Result> Handle(
        AddItemDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Add item description
        var result = item.AddDescription(
            request.Type,
            request.Description);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}