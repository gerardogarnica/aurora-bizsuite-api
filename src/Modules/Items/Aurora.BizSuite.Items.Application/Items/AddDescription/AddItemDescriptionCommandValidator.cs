namespace Aurora.BizSuite.Items.Application.Items.AddDescription;

internal sealed class AddItemDescriptionCommandValidator : AbstractValidator<AddItemDescriptionCommand>
{
    public AddItemDescriptionCommandValidator()
    {
        RuleFor(x => x.Type)
            .MaximumLength(40).WithBaseError(ItemErrors.TypeIsTooLong);

        RuleFor(x => x.Description)
            .MaximumLength(4000).WithBaseError(ItemErrors.ItemDescriptionIsTooLong);
    }
}