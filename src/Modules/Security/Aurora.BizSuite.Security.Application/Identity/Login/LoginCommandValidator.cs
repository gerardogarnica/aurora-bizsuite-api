namespace Aurora.BizSuite.Security.Application.Identity.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithBaseError(IdentityValidationErrors.EmailIsRequired)
            .EmailAddress().WithBaseError(IdentityValidationErrors.InvalidEmail);

        RuleFor(x => x.Password)
            .NotEmpty().WithBaseError(IdentityValidationErrors.PasswordIsRequired);

        RuleFor(x => x.Application)
            .NotEmpty().WithBaseError(IdentityValidationErrors.ApplicationIsRequired);
    }
}