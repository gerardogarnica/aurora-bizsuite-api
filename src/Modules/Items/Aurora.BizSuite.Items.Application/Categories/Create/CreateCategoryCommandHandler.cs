namespace Aurora.BizSuite.Items.Application.Categories.Create;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        // Create category
        var category = Category.Create(
            request.Name,
            request.Notes);

        await categoryRepository.InsertAsync(category);

        return category.Id.Value;
    }
}