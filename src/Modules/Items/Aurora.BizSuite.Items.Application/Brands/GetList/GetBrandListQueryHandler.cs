namespace Aurora.BizSuite.Items.Application.Brands.GetList;

internal sealed class GetBrandListQueryHandler(
    IBrandRepository brandRepository)
    : IQueryHandler<GetBrandListQuery, PagedResult<BrandModel>>
{
    public async Task<Result<PagedResult<BrandModel>>> Handle(
        GetBrandListQuery request,
        CancellationToken cancellationToken)
    {
        // Get paged brands
        var pagedBrands = await brandRepository.GetPagedAsync(
            request.PagedView,
            request.SearchTerms);

        // Return paged result
        return Result.Ok(new PagedResult<BrandModel>(
            pagedBrands.Items.Select(x => x.ToBrandModel()).ToList(),
            pagedBrands.TotalItems,
            pagedBrands.CurrentPage,
            pagedBrands.TotalPages));
    }
}