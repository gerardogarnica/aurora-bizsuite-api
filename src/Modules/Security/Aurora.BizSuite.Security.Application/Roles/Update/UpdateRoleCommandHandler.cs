using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Roles.Update;

internal class UpdateRoleCommandHandler(
    IRoleRepository roleRepository,
    IApplicationProvider applicationProvider)
    : ICommandHandler<UpdateRoleCommand>
{
    public async Task<Result> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        // Get application ID
        var applicationId = new ApplicationId(applicationProvider.GetApplicationId());

        // Get role
        var role = await roleRepository.GetByNameAsync(applicationId, request.Name.Trim());

        if (role is null)
            return Result.Fail(DomainErrors.RoleErrors.RoleNotFound(request.Name));

        // Update role
        var roleResult = role.Update(
            request.Name,
            request.Description,
            request.Notes);

        if (!roleResult.IsSuccessful)
            return Result.Fail(roleResult.Error);

        roleRepository.Update(roleResult.Value);

        return Result.Ok();
    }
}