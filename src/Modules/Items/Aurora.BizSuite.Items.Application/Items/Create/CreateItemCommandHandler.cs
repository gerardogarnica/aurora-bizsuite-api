namespace Aurora.BizSuite.Items.Application.Items.Create;

internal sealed class CreateItemCommandHandler(
    IItemRepository itemRepository,
    ICategoryRepository categoryRepository,
    IBrandRepository brandRepository)
    : ICommandHandler<CreateItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateItemCommand request,
        CancellationToken cancellationToken)
    {
        // Get the category
        var category = await categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId));
        if (category is null)
        {
            return Result.Fail<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        if (category.IsLocked)
        {
            return Result.Fail<Guid>(CategoryErrors.CategoryIsLocked);
        }

        if (!category.IsLeaf)
        {
            return Result.Fail<Guid>(CategoryErrors.CategoryIsNotLeaf);
        }

        // Get the brand
        var brand = await brandRepository.GetByIdAsync(new BrandId(request.BrandId));
        if (brand is null)
        {
            return Result.Fail<Guid>(BrandErrors.NotFound(request.BrandId));
        }

        if (brand.IsDeleted)
        {
            return Result.Fail<Guid>(BrandErrors.IsDeleted(request.BrandId));
        }

        // Create item
        var item = Item.Create(
            request.Code,
            request.Name,
            request.Description,
            category,
            brand,
            request.Type,
            request.AlternativeCode,
            request.Notes,
            request.Tags);

        await itemRepository.InsertAsync(item);

        return item.Id.Value;
    }
}