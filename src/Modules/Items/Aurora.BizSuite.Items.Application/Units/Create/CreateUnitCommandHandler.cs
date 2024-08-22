namespace Aurora.BizSuite.Items.Application.Units.Create;

internal sealed class CreateUnitCommandHandler(
    IUnitRepository unitRepository)
    : ICommandHandler<CreateUnitCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Create unit
        var unit = UnitOfMeasurement.Create(
            request.Name,
            request.Symbol,
            request.Notes);

        await unitRepository.InsertAsync(unit);

        return unit.Id.Value;
    }
}