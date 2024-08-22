namespace Aurora.BizSuite.Items.Application.Brands.Update;

internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(BrandErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(BrandErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(BrandErrors.NameIsTooLong);

        RuleFor(x => x.LogoUri)
            .MaximumLength(200).WithBaseError(BrandErrors.LogoUriIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(BrandErrors.NotesIsTooLong);
    }
}