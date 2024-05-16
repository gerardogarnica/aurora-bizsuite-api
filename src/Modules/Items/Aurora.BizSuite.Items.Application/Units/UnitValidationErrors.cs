namespace Aurora.BizSuite.Items.Application.Units;

internal static class UnitValidationErrors
{
    private const string ErrorCode = "Unit";

    internal static BaseError AcronymIsRequired => new(ErrorCode, "The unit acronym is required.");
    internal static BaseError AcronymIsTooLong => new(ErrorCode, "The maximum unit acronym length is 10 characters.");
    internal static BaseError NameIsRequired => new(ErrorCode, "The unit name is required.");
    internal static BaseError NameIsTooLong => new(ErrorCode, "The maximum unit name length is 100 characters.");
    internal static BaseError NameIsTooShort => new(ErrorCode, "The minimum unit name length is 3 characters.");
    internal static BaseError NotesIsTooLong => new(ErrorCode, "The maximum unit notes length is 1000 characters.");
}