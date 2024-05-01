namespace Aurora.BizSuite.Security.Application.Roles.Update;

public sealed record UpdateRoleCommand(
    Guid RoleId,
    string Name,
    string Description,
    string? Notes) : ICommand;