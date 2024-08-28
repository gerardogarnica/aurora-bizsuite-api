using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemUnit
{
    public Guid Id { get; private set; }
    public ItemId ItemId { get; private set; }
    public UnitOfMeasurementId UnitId { get; private set; }
    public bool IsPrimary { get; private set; }
    public bool AvailableForReceipt { get; private set; }
    public bool AvailableForDispatch { get; private set; }
    public bool UseDecimals { get; private set; }
    public bool IsEditable { get; private set; }
    public UnitOfMeasurement Unit { get; init; } = null!;

    private ItemUnit()
    {
        Id = Guid.NewGuid();
        ItemId = new ItemId(Guid.Empty);
        UnitId = new UnitOfMeasurementId(Guid.Empty);
    }

    internal static ItemUnit Create(
        ItemId itemId,
        UnitOfMeasurementId unitId,
        bool isPrimary,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        return new ItemUnit()
        {
            ItemId = itemId,
            UnitId = unitId,
            IsPrimary = isPrimary,
            AvailableForReceipt = availableForReceipt,
            AvailableForDispatch = availableForDispatch,
            UseDecimals = useDecimals,
            IsEditable = true
        };
    }

    internal void Update(
        bool isPrimary,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        IsPrimary = isPrimary;
        AvailableForReceipt = availableForReceipt;
        AvailableForDispatch = availableForDispatch;
        UseDecimals = useDecimals;
    }

    internal void SetIsEditable(bool isEditable) => IsEditable = isEditable;
}