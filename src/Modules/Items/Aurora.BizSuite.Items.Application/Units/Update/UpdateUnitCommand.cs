namespace Aurora.BizSuite.Items.Application.Units.Update;

public sealed record UpdateUnitCommand(
    Guid UnitId,
    string Name,
    string Symbol,
    string? Notes)
    : ICommand;