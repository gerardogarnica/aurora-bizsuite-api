namespace Aurora.BizSuite.Items.Application.Items.RemoveUnit;

internal sealed class RemoveItemUnitCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<RemoveItemUnitCommand>
{
    public async Task<Result> Handle(
        RemoveItemUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Remove item unit
        var result = item.RemoveUnit(request.ItemUnitId);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}