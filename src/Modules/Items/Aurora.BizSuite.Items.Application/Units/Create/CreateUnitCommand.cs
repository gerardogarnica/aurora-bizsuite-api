namespace Aurora.BizSuite.Items.Application.Units.Create;

public sealed record CreateUnitCommand(
    string Name,
    string Acronym,
    string? Notes) : ICommand<Guid>;