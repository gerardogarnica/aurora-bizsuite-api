namespace Aurora.BizSuite.Items.Application.Categories.Create;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Category? category;

        // Get the parent category
        Category? parentCategory;
        if (request.ParentId.HasValue)
        {
            var parentCategoryId = new CategoryId(request.ParentId.Value);
            parentCategory = await categoryRepository.GetByIdAsync(parentCategoryId);

            if (parentCategory == null)
            {
                return Result.Fail<Guid>(CategoryErrors.NotFound(request.ParentId.Value));
            }

            // Add child category
            category = parentCategory.AddChild(
                request.Name,
                request.Notes).Value;

            categoryRepository.Update(category);
        }
        else
        {
            // Create category
            category = Category.Create(
                request.Name,
                request.Notes);

            await categoryRepository.InsertAsync(category);
        }

        return category.Id.Value;
    }
}