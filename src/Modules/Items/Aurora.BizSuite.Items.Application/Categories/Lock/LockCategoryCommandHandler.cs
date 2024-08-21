namespace Aurora.BizSuite.Items.Application.Categories.Lock;

internal sealed class LockCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<LockCategoryCommand>
{
    public async Task<Result> Handle(
        LockCategoryCommand request,
        CancellationToken cancellationToken)
    {
        // Get category
        var category = await categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId));

        if (category is null)
        {
            return Result.Fail(CategoryErrors.NotFound(request.CategoryId));
        }

        // Lock category
        var result = category.Lock();

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        categoryRepository.Update(category);

        return Result.Ok();
    }
}