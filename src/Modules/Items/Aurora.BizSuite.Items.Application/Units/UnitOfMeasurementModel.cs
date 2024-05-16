namespace Aurora.BizSuite.Items.Application.Units;

public sealed record UnitOfMeasurementModel(
    Guid Id,
    string Name,
    string Acronym,
    string? Notes);

internal static class UnitModelExtensions
{
    internal static UnitOfMeasurementModel ToUnitModel(
        this UnitOfMeasurement unit)
    {
        return new UnitOfMeasurementModel(
            unit.Id.Value,
            unit.Name,
            unit.Acronym,
            unit.Notes);
    }
}