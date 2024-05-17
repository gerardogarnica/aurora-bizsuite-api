namespace Aurora.BizSuite.Items.Application.Units.Update;

internal sealed class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(UnitValidationErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(UnitValidationErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(UnitValidationErrors.NameIsTooLong);

        RuleFor(x => x.Acronym)
            .NotEmpty().WithBaseError(UnitValidationErrors.AcronymIsRequired)
            .MaximumLength(10).WithBaseError(UnitValidationErrors.AcronymIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UnitValidationErrors.NotesIsTooLong);
    }
}