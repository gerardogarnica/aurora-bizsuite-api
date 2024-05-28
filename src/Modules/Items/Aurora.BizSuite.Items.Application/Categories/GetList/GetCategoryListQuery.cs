namespace Aurora.BizSuite.Items.Application.Categories.GetList;

public sealed record GetCategoryListQuery(
    Guid? ParentId,
    string? SearchTerms)
    : IQuery<IReadOnlyCollection<CategoryModel>>;