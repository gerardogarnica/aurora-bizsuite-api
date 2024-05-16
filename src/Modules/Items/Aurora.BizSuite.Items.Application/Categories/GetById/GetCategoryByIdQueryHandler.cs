namespace Aurora.BizSuite.Items.Application.Categories.GetById;

internal sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, CategoryModel>
{
    public async Task<Result<CategoryModel>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get category
        var category = await categoryRepository.GetByIdAsync(new CategoryId(request.Id));

        if (category is null)
        {
            return Result.Fail<CategoryModel>(CategoryErrors.NotFound(request.Id));
        }

        // Return category model
        return Result.Ok(category.ToCategoryModel());
    }
}