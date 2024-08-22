namespace Aurora.BizSuite.Items.Application.Units.Create;

internal sealed class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    private readonly IUnitRepository _unitRepository;

    public CreateUnitCommandValidator(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(UnitErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(UnitErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(UnitErrors.NameIsTooLong)
            .MustAsync(BeUniqueName).WithBaseError(UnitErrors.NameIsNotUnique);

        RuleFor(x => x.Symbol)
            .NotEmpty().WithBaseError(UnitErrors.SymbolIsRequired)
            .MaximumLength(10).WithBaseError(UnitErrors.SymbolIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UnitErrors.NotesIsTooLong);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken = default!)
    {
        return await _unitRepository.GetByNameAsync(name) is null;
    }
}