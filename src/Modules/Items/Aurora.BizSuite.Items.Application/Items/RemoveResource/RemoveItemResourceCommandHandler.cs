namespace Aurora.BizSuite.Items.Application.Items.RemoveResource;

internal sealed class RemoveItemResourceCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<RemoveItemResourceCommand>
{
    public async Task<Result> Handle(
        RemoveItemResourceCommand request,
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
        var result = item.RemoveResource(request.ItemResourceId, request.Type);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}