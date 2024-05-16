namespace Aurora.BizSuite.Items.Application.Categories;

public sealed record CategoryModel(
    Guid Id,
    string Name,
    Guid ParentId,
    int LevelNumber,
    string? Notes);

internal static class CategoryModelExtensions
{
    internal static CategoryModel ToCategoryModel(
        this Category category)
    {
        return new CategoryModel(
            category.Id.Value,
            category.Name,
            category.ParentId.Value,
            category.LevelNumber,
            category.Notes);
    }
}