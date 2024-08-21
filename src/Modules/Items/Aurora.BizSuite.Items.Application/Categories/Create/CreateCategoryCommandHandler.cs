using Aurora.Framework;

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

        if (request.ParentId.HasValue)
        {
            // Get the parent category
            var parentCategoryId = new CategoryId(request.ParentId.Value);
            Category? parentCategory = await categoryRepository.GetByIdAsync(parentCategoryId);

            if (parentCategory is null)
            {
                return Result.Fail<Guid>(CategoryErrors.NotFound(request.ParentId.Value));
            }

            // Add child category
            var result = parentCategory.AddChild(
                request.Name,
                request.Notes);

            if (!result.IsSuccessful)
            {
                return Result.Fail<Guid>(result.Error);
            }

            category = result.Value;

            categoryRepository.Update(category);
        }
        else
        {
            var list = await categoryRepository.GetListAsync(null, null);

            // Create category
            category = Category.Create(
                request.Name,
                request.Notes,
                list.Count);

            await categoryRepository.InsertAsync(category);
        }

        return category.Id.Value;
    }
}