namespace Aurora.BizSuite.Items.Domain.Items;

public static class ItemErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Items.NotFound",
        $"The item with identifier {id} was not found.");

    public static BaseError AlternativeCodeIsTooLong => new(
        "Items.AlternativeCodeIsTooLong",
        "The maximum item alternative code length is 50 characters.");

    public static BaseError CodeIsNotUnique => new(
        "Items.CodeIsNotUnique",
        "The item code already exists for another item.");

    public static BaseError CodeIsRequired => new(
        "Items.CodeIsRequired",
        "The item code is required.");

    public static BaseError CodeIsTooLong => new(
        "Items.CodeIsTooLong",
        "The maximum item code length is 40 characters.");

    public static BaseError CodeIsTooShort => new(
        "Items.CodeIsTooShort",
        "The minimum item code length is 3 characters.");

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

    public static BaseError ItemDescriptionAlreadyExists => new(
        "Items.ItemDescriptionAlreadyExists",
        "The item description already exists.");

    public static BaseError ItemDescriptionNotFound(string id) => new(
        "Items.ItemDescriptionNotFound",
        $"There is no item description with the identifier {id}.");

    public static BaseError ItemAlreadyIsDisabled => new(
        "Items.ItemAlreadyIsDisabled",
        "The item is already disabled.");

    public static BaseError ItemImageNotFound(string id) => new(
        "Items.ItemImageNotFound",
        $"There is no item image with the identifier {id}.");

    public static BaseError ItemImageCannotUpdatedToDown => new(
        "Items.ItemImageCannotUpdatedToDown",
        "The item image cannot be updated to downwards.");

    public static BaseError ItemImageCannotUpdatedToUp => new(
        "Items.ItemImageCannotUpdatedToUp",
        "The item image cannot be updated to upwards.");

    public static BaseError ItemIsDisabled => new(
        "Items.ItemIsDisabled",
        "The item is disabled.");

    public static BaseError ItemUnitAlreadyExists => new(
        "Items.ItemUnitAlreadyExists",
        "The item unit already exists.");

    public static BaseError ItemUnitIsNotEditable => new(
        "Items.ItemUnitIsNotEditable",
        "The item unit is unable to change.");

    public static BaseError ItemUnitIsPrimary => new(
        "Items.ItemUnitIsPrimary",
        "The item unit is already the primary unit.");

    public static BaseError ItemUnitIsUnableToRemove => new(
        "Items.ItemUnitIsUnableToRemove",
        "The item unit is unable to remove because is the primary unit.");

    public static BaseError ItemWithoutUnits => new(
        "Items.ItemWithoutUnits",
        "The item has no units of measurement.");

    public static BaseError MaxNumberOfUnitsReached => new(
        "Items.MaxNumberOfUnitsReached",
        "The maximum number of units has been reached.");

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

    public static BaseError TagsIsTooLong => new(
        "Items.TagsIsTooLong",
        "The maximum item tag length is 40 characters.");

    public static BaseError TagsLimitExceeded => new(
        "Items.TagsLimitExceeded",
        "The maximum item tags is 100 tags.");
}