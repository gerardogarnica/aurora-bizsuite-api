namespace Aurora.BizSuite.Items.Domain.Categories;

public static class CategoryErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Categories.NotFound",
        $"The category with identifier {id} was not found.");

    public static BaseError ChildCategoryNotFound(Guid childId, string name) => new(
        "Categories.ChildCategoryNotFound",
        $"The category with identifier {childId} does not belong to the category '{name}'.");

    public static BaseError NameIsRequired => new(
        "Categories.NameIsRequired",
        "The category name is required.");

    public static BaseError NameIsTaken => new(
        "Categories.NameIsTaken",
        "The category name already exists and cannot be created again.");

    public static BaseError NameIsTooLong => new(
        "Categories.NameIsTooLong",
        "The maximum category name length is 100 characters.");

    public static BaseError NameIsTooShort => new(
        "Categories.NameIsTooShort",
        "The minimum category name length is 3 characters.");

    public static BaseError NotesIsTooLong => new(
        "Categories.NotesIsTooLong",
        "The maximum category notes length is 1000 characters.");
}