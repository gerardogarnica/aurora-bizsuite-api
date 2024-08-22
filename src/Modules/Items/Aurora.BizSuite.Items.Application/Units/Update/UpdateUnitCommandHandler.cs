namespace Aurora.BizSuite.Items.Application.Units.Update;

internal sealed class UpdateUnitCommandHandler(
    IUnitRepository unitRepository)
    : ICommandHandler<UpdateUnitCommand>
{
    public async Task<Result> Handle(
        UpdateUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Get unit
        var unitId = new UnitOfMeasurementId(request.UnitId);
        var unit = await unitRepository.GetByIdAsync(unitId);

        if (unit is null)
        {
            return Result.Fail(UnitErrors.NotFound(request.UnitId));
        }

        if (!await NameIsUnique(unit, request.Name))
        {
            return Result.Fail(UnitErrors.NameIsNotUnique);
        }

        // Update unit
        var result = unit.Update(
            request.Name,
            request.Symbol,
            request.Notes);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        unitRepository.Update(result.Value);

        return Result.Ok();
    }

    private async Task<bool> NameIsUnique(UnitOfMeasurement unit, string name)
    {
        var anotherUnit = await unitRepository.GetByNameAsync(name);

        if (anotherUnit is null)
        {
            return true;
        }

        if (anotherUnit.Id.Equals(unit.Id))
        {
            return true;
        }

        return false;
    }
}