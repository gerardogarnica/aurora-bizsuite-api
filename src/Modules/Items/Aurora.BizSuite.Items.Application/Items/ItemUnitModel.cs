using Aurora.BizSuite.Items.Application.Units;

namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemUnitModel
{
    public Guid ItemUnitId { get; internal set; }
    public Guid UnitId { get; internal set; }
    public UnitOfMeasurementModel? Unit { get; internal set; }
    public bool IsPrimary { get; internal set; }
    public bool AvailableForReceipt { get; internal set; }
    public bool AvailableForDispatch { get; internal set; }
    public bool UseDecimals { get; internal set; }
    public bool IsEditable { get; internal set; }
}

internal static class ItemUnitModelExtensions
{
    internal static ItemUnitModel ToItemUnitModel(this ItemUnit itemUnit)
    {
        return new ItemUnitModel
        {
            ItemUnitId = itemUnit.Id,
            UnitId = itemUnit.UnitId.Value,
            Unit = itemUnit.Unit.ToUnitModel(),
            IsPrimary = itemUnit.IsPrimary,
            AvailableForReceipt = itemUnit.AvailableForReceipt,
            AvailableForDispatch = itemUnit.AvailableForDispatch,
            UseDecimals = itemUnit.UseDecimals,
            IsEditable = itemUnit.IsEditable
        };
    }
}