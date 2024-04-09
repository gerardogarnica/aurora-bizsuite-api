namespace Aurora.BizSuite.Security.Application.Roles.GetById;

public class GetRoleByIdQueryHandler(
    IRoleRepository roleRepository)
    : IQueryHandler<GetRoleByIdQuery, RoleInfo>
{
    public async Task<Result<RoleInfo>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get role
        var role = await roleRepository.GetByIdAsync(new RoleId(request.Id));

        if (role is null)
            return Result.Fail<RoleInfo>(DomainErrors.RoleErrors.RoleNotFound(request.Id));

        // Return role info
        return role.ToRoleInfo();
    }
}