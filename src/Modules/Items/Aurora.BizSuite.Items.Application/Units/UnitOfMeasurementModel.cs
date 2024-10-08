﻿namespace Aurora.BizSuite.Items.Application.Units;

public sealed record UnitOfMeasurementModel(
    Guid Id,
    string Name,
    string Symbol,
    string? Notes);

internal static class UnitModelExtensions
{
    internal static UnitOfMeasurementModel ToUnitModel(this UnitOfMeasurement unit) => new(
        unit.Id.Value,
        unit.Name,
        unit.Symbol,
        unit.Notes);
}