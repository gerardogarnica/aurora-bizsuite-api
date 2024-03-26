namespace Aurora.BizSuite.Security.Application.Users.UpdateRole;

public sealed record UpdateUserRoleCommand(
    Guid UserId,
    Guid RoleId,
    bool IsActive) : ICommand;