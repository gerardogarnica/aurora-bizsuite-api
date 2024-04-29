using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Roles.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.ApplicationId)
            .NotEmpty().WithBaseError(RoleValidationErrors.ApplicationIdIsRequired);

        RuleFor(x => new { x.ApplicationId, x.Name })
            .MustAsync((x, cancel) => BeUniqueRoleName(x.ApplicationId, x.Name, cancel)).WithBaseError(RoleValidationErrors.NameAlreadyExists);

        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(RoleValidationErrors.NameIsRequired)
            .MaximumLength(50).WithBaseError(RoleValidationErrors.NameIsTooLong);

        RuleFor(x => x.Description)
            .NotEmpty().WithBaseError(RoleValidationErrors.DescriptionIsRequired)
            .MaximumLength(200).WithBaseError(RoleValidationErrors.DescriptionIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(RoleValidationErrors.NotesIsTooLong);
    }

    private async Task<bool> BeUniqueRoleName(Guid applicationId, string name, CancellationToken cancellationToken)
    {
        var application = new ApplicationId(applicationId);
        return await _roleRepository.GetByNameAsync(application, name) is null;
    }
}