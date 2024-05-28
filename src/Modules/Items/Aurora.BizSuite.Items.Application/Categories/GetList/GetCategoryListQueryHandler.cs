namespace Aurora.BizSuite.Items.Application.Categories.GetList;

internal sealed class GetCategoryListQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryListQuery, IReadOnlyCollection<CategoryModel>>
{
    public async Task<Result<IReadOnlyCollection<CategoryModel>>> Handle(
        GetCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        // Get categories
        IReadOnlyCollection<Category> categories = await categoryRepository.GetListAsync(
            request.ParentId,
            request.SearchTerms);

        // Return result
        IReadOnlyCollection<CategoryModel> categoryModelList = categories
            .Select(x => x.ToCategoryModel())
            .ToList();

        return Result.Ok(categoryModelList);
    }
}