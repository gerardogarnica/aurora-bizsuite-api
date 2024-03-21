namespace Aurora.BizSuite.Security.Application.Identity;

internal static class IdentityValidationErrors
{
    private const string ErrorCode = "Login";

    internal static BaseError ApplicationIsRequired => new(ErrorCode, "The application code is required.");
    internal static BaseError EmailIsRequired => new(ErrorCode, "The user email address is required.");
    internal static BaseError InvalidEmail => new(ErrorCode, "The user email address is not valid.");
    internal static BaseError PasswordIsRequired => new(ErrorCode, "The user password is required.");
}