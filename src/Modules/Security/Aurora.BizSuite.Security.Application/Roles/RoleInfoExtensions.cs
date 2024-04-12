namespace Aurora.BizSuite.Security.Application.Roles;

internal static class RoleInfoExtensions
{
    internal static RoleInfo ToRoleInfo(this Role role)
    {
        return new RoleInfo(
            role.Id.Value,
            role.ApplicationId.Value,
            role.Name,
            role.Description,
            role.Notes,
            !role.IsDeleted);
    }
}