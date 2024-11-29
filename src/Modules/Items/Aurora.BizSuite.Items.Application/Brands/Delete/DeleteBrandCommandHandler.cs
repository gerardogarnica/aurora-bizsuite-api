namespace Aurora.BizSuite.Items.Application.Brands.Delete;

internal sealed class DeleteBrandCommandHandler(
    IBrandRepository brandRepository)
    : ICommandHandler<DeleteBrandCommand>
{
    public async Task<Result> Handle(
        DeleteBrandCommand request, 
        CancellationToken cancellationToken)
    {
        // Get brand
        var brandId = new BrandId(request.BrandId);
        var brand = await brandRepository.GetByIdAsync(brandId);

        if (brand is null)
        {
            return Result.Fail(BrandErrors.NotFound(request.BrandId));
        }

        // Delete brand
        var result = brand.Delete();

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        brandRepository.Delete(brand);

        return Result.Ok();
    }
}