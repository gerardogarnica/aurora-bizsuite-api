namespace Aurora.BizSuite.Items.Application.Items.Create;

internal sealed class CreateItemCommandHandler(
    IItemRepository itemRepository,
    ICategoryRepository categoryRepository,
    IUnitRepository unitRepository)
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

        // Get the unit of measurement
        var mainUnit = await unitRepository.GetByIdAsync(new UnitOfMeasurementId(request.MainUnitId));
        if (mainUnit is null)
        {
            return Result.Fail<Guid>(UnitErrors.NotFound(request.MainUnitId));
        }

        // Create item
        var item = Item.Create(
            request.Code,
            request.Name,
            request.Description,
            category,
            request.Type,
            mainUnit,
            request.AlternativeCode,
            request.Notes,
            request.Tags);

        await itemRepository.InsertAsync(item);

        return item.Id.Value;
    }
}