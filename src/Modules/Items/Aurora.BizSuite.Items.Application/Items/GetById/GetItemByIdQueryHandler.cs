namespace Aurora.BizSuite.Items.Application.Items.GetById;

internal sealed class GetItemByIdQueryHandler(
    IItemRepository itemRepository)
    : IQueryHandler<GetItemByIdQuery, ItemModel>
{
    public async Task<Result<ItemModel>> Handle(
        GetItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get item
        var item = await itemRepository.GetByIdAsync(new ItemId(request.Id));

        if (item is null)
        {
            return Result.Fail<ItemModel>(ItemErrors.NotFound(request.Id));
        }

        // Return item model
        return Result.Ok(item.ToItemModel());
    }
}