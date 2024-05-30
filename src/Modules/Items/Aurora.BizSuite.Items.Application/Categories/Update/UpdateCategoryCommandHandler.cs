namespace Aurora.BizSuite.Items.Application.Categories.Update;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        // Get category
        var category = await categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId));

        if (category is null)
        {
            return Result.Fail(CategoryErrors.ChildCategoryNotFound(request.CategoryId, request.Name));
        }

        // Update category
        var result = category.Update(
            request.Name,
            request.Notes);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        categoryRepository.Update(category);

        return Result.Ok();
    }
}