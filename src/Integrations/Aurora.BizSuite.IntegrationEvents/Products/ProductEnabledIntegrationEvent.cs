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
    public IReadOnlyList<ProductUnitModel> ProductUnits { get; init; } = productUnits;
}

public sealed record ProductUnitModel(
    Guid ProductUnitId,
    Guid UnitId,
    bool IsPrimary,
    bool AvailableForReceipt,
    bool AvailableForDispatch,
    bool UseDecimals,
    bool IsEditable);