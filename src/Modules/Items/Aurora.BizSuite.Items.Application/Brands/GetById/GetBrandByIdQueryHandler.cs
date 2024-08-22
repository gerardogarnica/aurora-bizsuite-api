namespace Aurora.BizSuite.Items.Application.Brands.GetById;

internal sealed class GetBrandByIdQueryHandler(
    IBrandRepository brandRepository)
    : IQueryHandler<GetBrandByIdQuery, BrandModel>
{
    public async Task<Result<BrandModel>> Handle(
        GetBrandByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get brand
        var brandId = new BrandId(request.Id);
        var brand = await brandRepository.GetByIdAsync(brandId);

        if (brand is null)
        {
            return Result.Fail<BrandModel>(BrandErrors.NotFound(request.Id));
        }

        // Return brand model
        return Result.Ok(brand.ToBrandModel());
    }
}