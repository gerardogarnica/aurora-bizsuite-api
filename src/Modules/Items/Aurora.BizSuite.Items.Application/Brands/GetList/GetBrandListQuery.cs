namespace Aurora.BizSuite.Items.Application.Brands.GetList;

public sealed record GetBrandListQuery(
    PagedViewRequest PagedView,
    string? SearchTerms)
    : IQuery<PagedResult<BrandModel>>;