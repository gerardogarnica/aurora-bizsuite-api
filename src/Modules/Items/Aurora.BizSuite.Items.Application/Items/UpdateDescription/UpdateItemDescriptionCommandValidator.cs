namespace Aurora.BizSuite.Items.Application.Items.UpdateDescription;

internal sealed class UpdateItemDescriptionCommandValidator : AbstractValidator<UpdateItemDescriptionCommand>
{
    public UpdateItemDescriptionCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(4000).WithBaseError(ItemErrors.ItemDescriptionIsTooLong);
    }
}