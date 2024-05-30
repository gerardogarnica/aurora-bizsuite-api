namespace Aurora.BizSuite.Items.Application.Categories.Update;

internal sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(CategoryErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(CategoryErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(CategoryErrors.NameIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(CategoryErrors.NotesIsTooLong);
    }
}