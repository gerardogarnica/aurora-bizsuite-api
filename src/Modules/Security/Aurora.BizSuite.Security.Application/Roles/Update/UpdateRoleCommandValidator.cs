namespace Aurora.BizSuite.Security.Application.Roles.Update;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(RoleValidationErrors.NameIsRequired)
            .MaximumLength(50).WithBaseError(RoleValidationErrors.NameIsTooLong);

        RuleFor(x => x.Description)
            .NotEmpty().WithBaseError(RoleValidationErrors.DescriptionIsRequired)
            .MaximumLength(200).WithBaseError(RoleValidationErrors.DescriptionIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(RoleValidationErrors.NotesIsTooLong);
    }
}