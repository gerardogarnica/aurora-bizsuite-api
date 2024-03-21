namespace Aurora.BizSuite.Security.Application.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithBaseError(UserValidationErrors.EmailIsRequired)
            .EmailAddress().WithBaseError(UserValidationErrors.InvalidEmail);

        RuleFor(x => x.FirstName)
            .NotEmpty().WithBaseError(UserValidationErrors.FirstNameIsRequired)
            .MaximumLength(20).WithBaseError(UserValidationErrors.FirstNameIsTooLong);

        RuleFor(x => x.LastName)
            .NotEmpty().WithBaseError(UserValidationErrors.LastNameIsRequired)
            .MaximumLength(20).WithBaseError(UserValidationErrors.LastNameIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UserValidationErrors.NotesIsTooLong);
    }
}