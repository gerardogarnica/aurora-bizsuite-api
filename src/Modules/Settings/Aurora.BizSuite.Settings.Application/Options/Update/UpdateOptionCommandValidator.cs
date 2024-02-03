namespace Aurora.BizSuite.Settings.Application.Options.Update;

internal class UpdateOptionCommandValidator : AbstractValidator<UpdateOptionCommand>
{
    public UpdateOptionCommandValidator(IOptionRepository optionRepository)
    {
        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}