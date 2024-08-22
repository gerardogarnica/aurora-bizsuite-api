namespace Aurora.BizSuite.Items.Application.Items.Update;

internal sealed class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(ItemErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(ItemErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(ItemErrors.NameIsTooLong);

        RuleFor(x => x.Description)
            .NotEmpty().WithBaseError(ItemErrors.DescriptionIsRequired)
            .MinimumLength(3).WithBaseError(ItemErrors.DescriptionIsTooShort)
            .MaximumLength(1000).WithBaseError(ItemErrors.DescriptionIsTooLong);

        RuleFor(x => x.AlternativeCode)
            .MaximumLength(40).WithBaseError(ItemErrors.AlternativeCodeIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(ItemErrors.NotesIsTooLong);

        RuleFor(x => x.Tags)
            .Must(x => x.Count <= 100).WithBaseError(ItemErrors.TagsLimitExceeded);

        RuleForEach(x => x.Tags)
            .MaximumLength(40).WithBaseError(ItemErrors.TagsIsTooLong);
    }
}