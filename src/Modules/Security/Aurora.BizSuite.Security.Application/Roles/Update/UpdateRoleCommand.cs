namespace Aurora.BizSuite.Security.Application.Roles.Update;

public sealed record UpdateRoleCommand(
    string Name,
    string Description,
    string? Notes) : ICommand;