namespace Aurora.BizSuite.Items.Application.Brands.GetList;

public sealed record GetBrandListQuery(
    PagedViewRequest PagedView,
    string? SearchTerms,
    bool ShowDeleted)
    : IQuery<PagedResult<BrandModel>>;