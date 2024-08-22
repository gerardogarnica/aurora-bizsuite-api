namespace Aurora.BizSuite.Items.Application.Items.GetList;

internal sealed class GetItemListQueryHandler(
    IItemRepository itemRepository)
    : IQueryHandler<GetItemListQuery, PagedResult<ItemModel>>
{
    public async Task<Result<PagedResult<ItemModel>>> Handle(
        GetItemListQuery request,
        CancellationToken cancellationToken)
    {
        // Get category ID
        CategoryId? categoryId = request.CategoryId.HasValue
            ? new CategoryId(request.CategoryId.Value)
            : null;

        // Get brand ID
        BrandId? brandId = request.BrandId.HasValue
            ? new BrandId(request.BrandId.Value)
            : null;

        // Get paged items
        var pagedItems = await itemRepository.GetPagedAsync(
            request.PagedView,
            categoryId,
            brandId,
            request.Type,
            request.Status,
            request.SearchTerms);

        // Return paged result
        return Result.Ok(new PagedResult<ItemModel>(
            pagedItems.Items.Select(x => x.ToItemModel()).ToList(),
            pagedItems.TotalItems,
            pagedItems.CurrentPage,
            pagedItems.TotalPages));
    }
}