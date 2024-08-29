namespace Aurora.BizSuite.Items.Application.Items.AddResource;

internal sealed class AddItemResourceCommandValidator : AbstractValidator<AddItemResourceCommand>
{
    public AddItemResourceCommandValidator()
    {
        RuleFor(x => x.Uri)
            .NotEmpty().WithBaseError(ItemErrors.UriIsRequired)
            .Must(IsValidUri).WithBaseError(ItemErrors.UriIsInvalid)
            .MaximumLength(1000).WithBaseError(ItemErrors.UriIsTooLong);

        RuleFor(x => x.Name)
            .MaximumLength(100).WithBaseError(ItemErrors.NameIsTooLong);
    }

    private bool IsValidUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out _);
    }
}