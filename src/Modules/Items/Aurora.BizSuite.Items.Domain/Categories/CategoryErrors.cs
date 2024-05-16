namespace Aurora.BizSuite.Items.Domain.Categories;

public static class CategoryErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Categories.NotFound",
        $"The category with identifier {id} was not found.");
}