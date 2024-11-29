namespace Aurora.BizSuite.Items.Application.Brands.Create;

internal sealed class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    private readonly IBrandRepository _brandRepository;

    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(BrandErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(BrandErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(BrandErrors.NameIsTooLong)
            .MustAsync(BeUniqueName).WithBaseError(BrandErrors.NameIsNotUnique);

        RuleFor(x => x.LogoUri)
            .MaximumLength(1000).WithBaseError(BrandErrors.LogoUriIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(BrandErrors.NotesIsTooLong);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken = default!)
    {
        var brand = await _brandRepository.GetByNameAsync(name);

        if (brand is null) return true;

        return brand.IsDeleted;
    }
}