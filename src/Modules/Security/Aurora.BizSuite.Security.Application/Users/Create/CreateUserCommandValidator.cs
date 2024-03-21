namespace Aurora.BizSuite.Security.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .NotEmpty().WithBaseError(UserValidationErrors.EmailIsRequired)
            .MustAsync(BeUniqueEmail).WithBaseError(UserValidationErrors.EmailAlreadyExists)
            .EmailAddress().WithBaseError(UserValidationErrors.InvalidEmail)
            .MaximumLength(50).WithBaseError(UserValidationErrors.EmailIsTooLong);

        RuleFor(x => x.FirstName)
            .NotEmpty().WithBaseError(UserValidationErrors.FirstNameIsRequired)
            .MaximumLength(20).WithBaseError(UserValidationErrors.FirstNameIsTooLong);

        RuleFor(x => x.LastName)
            .NotEmpty().WithBaseError(UserValidationErrors.LastNameIsRequired)
            .MaximumLength(20).WithBaseError(UserValidationErrors.LastNameIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UserValidationErrors.NotesIsTooLong);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(email) is null;
    }
}