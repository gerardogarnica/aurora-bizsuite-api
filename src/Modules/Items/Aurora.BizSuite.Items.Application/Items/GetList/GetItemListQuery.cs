namespace Aurora.BizSuite.Items.Application.Items.GetList;

public sealed record GetItemListQuery(
    PagedViewRequest PagedView,
    Guid? CategoryId,
    Guid? BrandId,
    ItemType? Type,
    ItemStatus? Status,
    string? SearchTerms)
    : IQuery<PagedResult<ItemModel>>;