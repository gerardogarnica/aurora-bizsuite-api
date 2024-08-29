namespace Aurora.BizSuite.Items.Domain.Brands;

public static class BrandErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Brands.NotFound",
        $"The brand with identifier {id} was not found.");

    public static BaseError LogoUriIsTooLong => new(
        "Brands.LogoUriIsTooLong",
        "The maximum brand logo URI length is 1000 characters.");

    public static BaseError NameIsNotUnique => new(
        "Brands.NameIsNotUnique",
        "The brand name already exists for another brand.");

    public static BaseError NameIsRequired => new(
        "Brands.NameIsRequired",
        "The brand name is required.");

    public static BaseError NameIsTooLong => new(
        "Brands.NameIsTooLong",
        "The maximum brand name length is 100 characters.");

    public static BaseError NameIsTooShort => new(
        "Brands.NameIsTooShort",
        "The minimum brand name length is 3 characters.");

    public static BaseError NotesIsTooLong => new(
        "Brands.NotesIsTooLong",
        "The maximum brand notes length is 1000 characters.");
}