using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Roles.Create;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IApplicationRepository applicationRepository,
    IApplicationProvider applicationProvider)
    : ICommandHandler<CreateRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        // Get application
        var applicationId = new ApplicationId(request.ApplicationId);
        var application = await applicationRepository.GetByIdAsync(applicationId);
        if (application is null)
            return Result.Fail<Guid>(DomainErrors.ApplicationErrors.ApplicationNotFound(request.ApplicationId));

        // Validate application
        if (!applicationProvider.IsAdminApp()
            && applicationId.Value.Equals(applicationProvider.GetApplicationId()))
        {
            return Result.Fail<Guid>(RoleValidationErrors.ApplicationIdIsNotValid);
        }

        // Create role
        var role = Role.Create(
            applicationId,
            request.Name,
            request.Description,
            request.Notes);

        await roleRepository.InsertAsync(role);

        return role.Id.Value;
    }
}