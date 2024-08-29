using Aurora.BizSuite.Items.Application.Units;

namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemUnitModel(
    Guid Id,
    Guid UnitId,
    UnitOfMeasurementModel? Unit,
    bool IsPrimary,
    bool AvailableForReceipt,
    bool AvailableForDispatch,
    bool UseDecimals,
    bool IsEditable);

internal static class ItemUnitModelExtensions
{
    internal static ItemUnitModel ToItemUnitModel(this ItemUnit itemUnit) => new(
        itemUnit.Id,
        itemUnit.UnitId.Value,
        itemUnit.Unit.ToUnitModel(),
        itemUnit.IsPrimary,
        itemUnit.AvailableForReceipt,
        itemUnit.AvailableForDispatch,
        itemUnit.UseDecimals,
        itemUnit.IsEditable);
}