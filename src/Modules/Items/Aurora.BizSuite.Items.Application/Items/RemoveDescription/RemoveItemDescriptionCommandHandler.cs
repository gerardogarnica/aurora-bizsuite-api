namespace Aurora.BizSuite.Items.Application.Items.RemoveDescription;

internal sealed class RemoveItemDescriptionCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<RemoveItemDescriptionCommand>
{
    public async Task<Result> Handle(
        RemoveItemDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Remove item description
        var result = item.RemoveDescription(request.ItemDescriptionId);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}