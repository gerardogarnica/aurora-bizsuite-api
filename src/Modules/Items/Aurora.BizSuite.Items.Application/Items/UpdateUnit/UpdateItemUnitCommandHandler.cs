namespace Aurora.BizSuite.Items.Application.Items.UpdateUnit;

internal sealed class UpdateItemUnitCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<UpdateItemUnitCommand>
{
    public async Task<Result> Handle(
        UpdateItemUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Update item unit
        var result = item.UpdateUnit(
            request.ItemUnitId,
            request.AvailableForReceipt,
            request.AvailableForDispatch,
            request.UseDecimals);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}