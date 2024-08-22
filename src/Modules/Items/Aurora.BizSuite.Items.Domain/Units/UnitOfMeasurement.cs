namespace Aurora.BizSuite.Items.Domain.Units;

public sealed class UnitOfMeasurement : AggregateRoot<UnitOfMeasurementId>, IAuditableEntity
{
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    private UnitOfMeasurement() : base(new UnitOfMeasurementId(Guid.NewGuid()))
    {
        Name = string.Empty;
        Symbol = string.Empty;
    }

    public static UnitOfMeasurement Create(
        string name,
        string symbol,
        string? notes)
    {
        var unitOfMeasurement = new UnitOfMeasurement
        {
            Name = name.Trim(),
            Symbol = symbol.Trim(),
            Notes = notes?.Trim()
        };

        return unitOfMeasurement;
    }

    public Result<UnitOfMeasurement> Update(
        string name,
        string symbol,
        string? notes)
    {
        Name = name.Trim();
        Symbol = symbol.Trim();
        Notes = notes?.Trim();

        return this;
    }
}