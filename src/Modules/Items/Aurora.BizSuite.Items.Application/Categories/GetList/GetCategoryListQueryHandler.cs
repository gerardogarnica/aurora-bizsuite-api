namespace Aurora.BizSuite.Items.Application.Categories.GetList;

internal sealed class GetCategoryListQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryListQuery, IReadOnlyCollection<CategoryModel>>
{
    public async Task<Result<IReadOnlyCollection<CategoryModel>>> Handle(
        GetCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        // Get category ID
        CategoryId? categoryId = request.ParentId.HasValue
            ? new CategoryId(request.ParentId.Value)
            : null;

        // Get categories
        IReadOnlyCollection<Category> categories = await categoryRepository.GetListAsync(
            categoryId,
            request.SearchTerms);

        // Return result
        IReadOnlyCollection<CategoryModel> categoryModelList = categories
            .Select(x => x.ToCategoryModel())
            .ToList();

        return Result.Ok(categoryModelList);
    }
}