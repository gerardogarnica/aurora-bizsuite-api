namespace Aurora.BizSuite.Items.Application.Units.Create;

public sealed record CreateUnitCommand(
    string Name,
    string Symbol,
    string? Notes)
    : ICommand<Guid>;