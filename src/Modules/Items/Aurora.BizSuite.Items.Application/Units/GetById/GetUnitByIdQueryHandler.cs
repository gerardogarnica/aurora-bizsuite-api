namespace Aurora.BizSuite.Items.Application.Units.GetById;

internal sealed class GetUnitByIdQueryHandler(
    IUnitRepository unitRepository)
    : IQueryHandler<GetUnitByIdQuery, UnitOfMeasurementModel>
{
    public async Task<Result<UnitOfMeasurementModel>> Handle(
        GetUnitByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get unit
        var unit = await unitRepository.GetByIdAsync(new UnitOfMeasurementId(request.Id));

        if (unit is null)
        {
            return Result.Fail<UnitOfMeasurementModel>(UnitErrors.NotFound(request.Id));
        }

        // Return unit model
        return Result.Ok(unit.ToUnitModel());
    }
}