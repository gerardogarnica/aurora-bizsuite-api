﻿namespace Aurora.BizSuite.Items.Application.Units.Update;

internal sealed class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(UnitErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(UnitErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(UnitErrors.NameIsTooLong);

        RuleFor(x => x.Symbol)
            .NotEmpty().WithBaseError(UnitErrors.SymbolIsRequired)
            .MaximumLength(10).WithBaseError(UnitErrors.SymbolIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(UnitErrors.NotesIsTooLong);
    }
}