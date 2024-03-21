namespace Aurora.BizSuite.Security.Application.Users;

internal static class UserValidationErrors
{
    private const string ErrorCode = "User";

    internal static BaseError EmailAlreadyExists=> new(ErrorCode, "The user email address already exists and cannot be created again.");
    internal static BaseError EmailIsRequired => new(ErrorCode, "The user email address is required.");
    internal static BaseError EmailIsTooLong => new(ErrorCode, "The maximum user email address length is 50 characters.");
    internal static BaseError FirstNameIsRequired => new(ErrorCode, "The user first name is required.");
    internal static BaseError FirstNameIsTooLong => new(ErrorCode, "The maximum user first name length is 20 characters.");
    internal static BaseError InvalidEmail => new(ErrorCode, "The user email address is not valid.");
    internal static BaseError LastNameIsRequired => new(ErrorCode, "The user last name is required.");
    internal static BaseError LastNameIsTooLong => new(ErrorCode, "The maximum user last name length is 20 characters.");
    internal static BaseError NotesIsTooLong => new(ErrorCode, "The maximum user notes length is 1000 characters.");
    internal static BaseError PasswordIsRequired => new(ErrorCode, "The user password is required.");
}