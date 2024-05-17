namespace Aurora.BizSuite.Items.Domain.Units;

public class UnitOfMeasurement : AggregateRoot<UnitOfMeasurementId>
{
    public string Name { get; private set; }
    public string Acronym { get; private set; }
    public string? Notes { get; private set; }

    protected UnitOfMeasurement()
        : base(new UnitOfMeasurementId(Guid.NewGuid()))
    {
        Name = string.Empty;
        Acronym = string.Empty;
    }

    public static UnitOfMeasurement Create(
        string name,
        string acronym,
        string? notes)
    {
        var unitOfMeasurement = new UnitOfMeasurement
        {
            Name = name.Trim(),
            Acronym = acronym.Trim(),
            Notes = notes?.Trim()
        };

        return unitOfMeasurement;
    }

    public Result<UnitOfMeasurement> Update(
        string name,
        string acronym,
        string? notes)
    {
        Name = name.Trim();
        Acronym = acronym.Trim();
        Notes = notes?.Trim();

        return this;
    }
}