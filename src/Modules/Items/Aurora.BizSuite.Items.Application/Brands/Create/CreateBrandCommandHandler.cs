namespace Aurora.BizSuite.Items.Application.Brands.Create;

internal sealed class CreateBrandCommandHandler(
    IBrandRepository brandRepository)
    : ICommandHandler<CreateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken)
    {
        // Create brand
        var brand = Brand.Create(
            request.Name,
            request.LogoUri,
            request.Notes);

        await brandRepository.InsertAsync(brand);

        return brand.Id.Value;
    }
}