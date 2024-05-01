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
        // Get role
        var roleId = new RoleId(request.RoleId);
        var role = await roleRepository.GetByIdAsync(roleId);

        if (role is null)
            return Result.Fail(DomainErrors.RoleErrors.RoleNotFound(request.Name));

        // Validate application
        if (!applicationProvider.IsAdminApp()
            && role.ApplicationId.Value.Equals(applicationProvider.GetApplicationId()))
        {
            return Result.Fail<Guid>(RoleValidationErrors.ApplicationIdIsNotValid);
        }

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