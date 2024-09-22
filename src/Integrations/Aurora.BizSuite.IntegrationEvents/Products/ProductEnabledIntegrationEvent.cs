namespace Aurora.BizSuite.IntegrationEvents.Products;

public sealed class ProductEnabledIntegrationEvent(
    Guid id,
    DateTime occurredOnUtc,
    Guid productId,
    string code,
    string name,
    string description,
    string? alternativeCode,
    List<ProductUnitModel> productUnits)
    : IntegrationEvent(id, occurredOnUtc)
{
    public Guid ProductId { get; init; } = productId;
    public string Code { get; init; } = code;
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public string? AlternativeCode { get; init; } = alternativeCode;
    public List<ProductUnitModel> ProductUnits { get; init; } = productUnits;
}

public sealed class ProductUnitModel
{
    public Guid ProductUnitId { get; init; }
    public Guid UnitId { get; init; }
    public bool IsPrimary { get; init; }
    public bool AvailableForReceipt { get; init; }
    public bool AvailableForDispatch { get; init; }
    public bool UseDecimals { get; init; }
    public bool IsEditable { get; init; }
}