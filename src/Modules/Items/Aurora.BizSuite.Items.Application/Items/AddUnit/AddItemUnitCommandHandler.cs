namespace Aurora.BizSuite.Items.Application.Items.AddUnit;

internal sealed class AddItemUnitCommandHandler(
    IItemRepository itemRepository,
    IUnitRepository unitRepository)
    : ICommandHandler<AddItemUnitCommand>
{
    public async Task<Result> Handle(
        AddItemUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Get unit
        var unitId = new UnitOfMeasurementId(request.UnitId);
        var unit = await unitRepository.GetByIdAsync(unitId);

        if (unit is null)
        {
            return Result.Fail(UnitErrors.NotFound(request.UnitId));
        }

        // Add item unit
        var result = item.AddUnit(
            unit,
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