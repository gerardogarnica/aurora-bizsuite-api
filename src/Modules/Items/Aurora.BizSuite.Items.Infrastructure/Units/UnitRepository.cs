namespace Aurora.BizSuite.Items.Infrastructure.Units;

internal sealed class UnitRepository(
    ItemsDbContext dbContext)
    : BaseRepository<UnitOfMeasurement, UnitOfMeasurementId>(dbContext), IUnitRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public async Task<UnitOfMeasurement?> GetByNameAsync(string name) => await dbContext
        .Units
        .Where(x => x.Name == name)
        .FirstOrDefaultAsync();
}