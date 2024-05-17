namespace Aurora.BizSuite.Items.Application.Units.Create;

internal sealed class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    private readonly IUnitRepository _unitRepository;

    public CreateUnitCommandValidator(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(UnitValidationErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(UnitValidationErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(UnitValidationErrors.NameIsTooLong)
            .MustAsync(BeUniqueName).WithBaseError(UnitValidationErrors.NameIsTaken);

        RuleFor(x => x.Acronym)
            .NotEmpty().WithBaseError(UnitValidationErrors.AcronymIsRequired)
            .MaximumLength(10).WithBaseError(UnitValidationErrors.AcronymIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UnitValidationErrors.NotesIsTooLong);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken = default!)
    {
        return await _unitRepository.GetByNameAsync(name) is null;
    }
}