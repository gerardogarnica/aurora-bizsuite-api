namespace Aurora.BizSuite.Items.Application.Brands.Update;

internal sealed class UpdateBrandCommandHandler(
    IBrandRepository brandRepository)
    : ICommandHandler<UpdateBrandCommand>
{
    public async Task<Result> Handle(
        UpdateBrandCommand request,
        CancellationToken cancellationToken)
    {
        // Get brand
        var brandId = new BrandId(request.BrandId);
        var brand = await brandRepository.GetByIdAsync(brandId);

        if (brand is null)
        {
            return Result.Fail(BrandErrors.NotFound(request.BrandId));
        }

        if (!await NameIsUnique(brand, request.Name))
        {
            return Result.Fail(BrandErrors.NameIsNotUnique);
        }

        // Update brand
        var result = brand.Update(
            request.Name,
            request.LogoUri,
            request.Notes);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        brandRepository.Update(brand);

        return Result.Ok();
    }

    private async Task<bool> NameIsUnique(Brand brand, string name)
    {
        var anotherBrand = await brandRepository.GetByNameAsync(name);

        if (anotherBrand is null)
        {
            return true;
        }

        if (anotherBrand.Id.Equals(brand.Id))
        {
            return true;
        }

        return false;
    }
}