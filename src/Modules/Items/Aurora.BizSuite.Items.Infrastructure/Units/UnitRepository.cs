namespace Aurora.BizSuite.Items.Infrastructure.Units;

internal sealed class UnitRepository(
    ItemsDbContext dbContext)
    : BaseRepository<UnitOfMeasurement, UnitOfMeasurementId>(dbContext), IUnitRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
}