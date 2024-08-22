namespace Aurora.BizSuite.Items.Domain.Units;

public static class UnitErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Units.NotFound",
        $"The unit with identifier {id} was not found.");

    public static BaseError NameIsNotUnique => new(
        "Units.NameIsNotUnique",
        "The unit name already exists for another unit of measurement.");

    public static BaseError NameIsRequired => new(
        "Units.NameIsRequired",
        "The unit name is required.");

    public static BaseError NameIsTooLong => new(
        "Units.NameIsTooLong",
        "The maximum unit name length is 100 characters.");

    public static BaseError NameIsTooShort => new(
        "Units.NameIsTooShort",
        "The minimum unit name length is 3 characters.");

    public static BaseError NotesIsTooLong => new(
        "Units.NotesIsTooLong",
        "The maximum unit notes length is 1000 characters.");

    public static BaseError SymbolIsRequired => new(
        "Units.SymbolIsRequired",
        "The unit symbol is required.");

    public static BaseError SymbolIsTooLong => new(
        "Units.SymbolIsTooLong",
        "The maximum unit symbol length is 10 characters.");
}