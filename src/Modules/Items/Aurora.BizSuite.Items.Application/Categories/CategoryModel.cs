namespace Aurora.BizSuite.Items.Application.Categories;

public sealed record CategoryModel(
    Guid Id,
    string Name,
    Guid? ParentId,
    int Level,
    string? Notes,
    IReadOnlyCollection<CategoryModel> Childs);

internal static class CategoryModelExtensions
{
    internal static CategoryModel ToCategoryModel(
        this Category category)
    {
        return new CategoryModel(
            category.Id.Value,
            category.Name,
            category.ParentId?.Value,
            category.Level,
            category.Notes,
            category.Childs.Select(x => x.ToCategoryModel()).ToList());
    }
}