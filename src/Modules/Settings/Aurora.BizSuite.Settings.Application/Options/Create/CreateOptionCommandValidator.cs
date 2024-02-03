namespace Aurora.BizSuite.Settings.Application.Options.Create;

internal class CreateOptionCommandValidator : AbstractValidator<CreateOptionCommand>
{
    private readonly IOptionRepository _optionRepository;

    public CreateOptionCommandValidator(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));

        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(40)
            .MustAsync(BeUniqueCode);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(200);

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleForEach(x => x.Items)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Code)
                    .NotEmpty()
                    .MaximumLength(40);

                x.RuleFor(x => x.Description)
                    .MaximumLength(200);
            });
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        var option = await _optionRepository.GetOptionAsync(code);

        return option is null;
    }
}