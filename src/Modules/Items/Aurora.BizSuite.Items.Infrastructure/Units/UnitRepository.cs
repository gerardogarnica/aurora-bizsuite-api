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

    public async Task<PagedResult<UnitOfMeasurement>> GetPagedAsync(
        PagedViewRequest paged,
        string? searchTerms)
    {
        var query = dbContext
            .Units
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(x =>
                x.Name.Contains(searchTerms)
                || x.Acronym.Contains(searchTerms));
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}