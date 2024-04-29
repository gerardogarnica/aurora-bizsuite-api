namespace Aurora.BizSuite.Security.Application.Roles.Create;

public sealed record CreateRoleCommand(
    Guid ApplicationId,
    string Name,
    string Description,
    string? Notes) : ICommand<Guid>;