namespace Aurora.BizSuite.Items.Application.Categories;

public sealed record CategoryModel(
    Guid Id,
    string Name,
    string Code,
    Guid? ParentId,
    int Level,
    string? Notes,
    bool IsLocked,
    bool IsLeaf,
    IReadOnlyCollection<CategoryModel> Childs);

internal static class CategoryModelExtensions
{
    internal static CategoryModel ToCategoryModel(
        this Category category)
    {
        return new CategoryModel(
            category.Id.Value,
            category.Name,
            category.Code,
            category.ParentId?.Value,
            category.Level,
            category.Notes,
            category.IsLocked,
            category.IsLeaf,
            category.Childs.Select(x => x.ToCategoryModel()).ToList());
    }
}