using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemUnit
{
    public int Id { get; private set; }
    public ItemId ItemId { get; private set; }
    public UnitOfMeasurementId UnitId { get; private set; }
    public decimal ConversionValue { get; private set; }
    public bool AvailableForReceipt { get; private set; }
    public bool AvailableForDispatch { get; private set; }
    public bool UseDecimals { get; private set; }

    private ItemUnit()
    {
        ItemId = new ItemId(Guid.Empty);
        UnitId = new UnitOfMeasurementId(Guid.Empty);
    }

    internal ItemUnit(
        ItemId itemId,
        UnitOfMeasurementId unitId,
        decimal conversionValue,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        ItemId = itemId;
        UnitId = unitId;
        ConversionValue = conversionValue;
        AvailableForReceipt = availableForReceipt;
        AvailableForDispatch = availableForDispatch;
        UseDecimals = useDecimals;
    }

    internal void Update(
        decimal conversionValue,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        ConversionValue = conversionValue;
        AvailableForReceipt = availableForReceipt;
        AvailableForDispatch = availableForDispatch;
        UseDecimals = useDecimals;
    }
}