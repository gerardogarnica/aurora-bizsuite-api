using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Roles.Create;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IApplicationProvider applicationProvider)
    : ICommandHandler<CreateRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        // Validate application
        if (!applicationProvider.IsAdminApp()
            && request.ApplicationId.Equals(applicationProvider.GetApplicationId()))
        {
            return Result.Fail<Guid>(RoleValidationErrors.ApplicationIdIsNotValid);
        }

        // Create role
        var role = Role.Create(
            new ApplicationId(request.ApplicationId),
            request.Name,
            request.Description,
            request.Notes);

        await roleRepository.InsertAsync(role);

        return role.Id.Value;
    }
}