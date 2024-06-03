namespace Aurora.BizSuite.Items.Domain.Items;

public static class ItemErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Items.NotFound",
        $"The item with identifier {id} was not found.");

    public static BaseError AlternCodeIsTooLong => new(
        "Items.AlternCodeIsTooLong",
        "The maximum item alternative code length is 50 characters.");

    public static BaseError DescriptionIsRequired => new(
        "Items.DescriptionIsRequired",
        "The item description is required.");

    public static BaseError DescriptionIsTooLong => new(
        "Items.DescriptionIsTooLong",
        "The maximum item description length is 1000 characters.");

    public static BaseError DescriptionIsTooShort => new(
        "Items.DescriptionIsTooShort",
        "The minimum item description length is 3 characters.");

    public static BaseError ItemAlreadyIsActive => new(
        "Items.ItemAlreadyIsActive",
        "The item is already active.");

    public static BaseError NameIsRequired => new(
        "Items.NameIsRequired",
        "The item name is required.");

    public static BaseError NameIsTaken => new(
        "Items.NameIsTaken",
        "The item name already exists and cannot be created again.");

    public static BaseError NameIsTooLong => new(
        "Items.NameIsTooLong",
        "The maximum item name length is 100 characters.");

    public static BaseError NameIsTooShort => new(
        "Items.NameIsTooShort",
        "The minimum item name length is 3 characters.");

    public static BaseError NotesIsTooLong => new(
        "Items.NotesIsTooLong",
        "The maximum item notes length is 1000 characters.");
}