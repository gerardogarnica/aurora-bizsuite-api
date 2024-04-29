namespace Aurora.BizSuite.Security.Application.Roles;

internal static class RoleValidationErrors
{
    private const string ErrorCode = "Role";

    internal static BaseError ApplicationIdIsRequired => new(ErrorCode, "The role application ID is required.");
    internal static BaseError ApplicationIdIsNotValid => new(ErrorCode, "The role application ID is not valid.");
    internal static BaseError DescriptionIsRequired => new(ErrorCode, "The role description is required.");
    internal static BaseError DescriptionIsTooLong => new(ErrorCode, "The maximum role description length is 200 characters.");
    internal static BaseError NameAlreadyExists => new(ErrorCode, "The role name already exists and cannot be created again.");
    internal static BaseError NameIsRequired => new(ErrorCode, "The role name is required.");
    internal static BaseError NameIsTooLong => new(ErrorCode, "The maximum role name length is 50 characters.");
    internal static BaseError NotesIsTooLong => new(ErrorCode, "The maximum role notes length is 1000 characters.");
}