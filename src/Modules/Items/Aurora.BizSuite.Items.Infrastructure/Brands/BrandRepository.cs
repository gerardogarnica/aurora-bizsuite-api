namespace Aurora.BizSuite.Items.Infrastructure.Brands;

internal sealed class BrandRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Brand, BrandId>(dbContext), IBrandRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public async Task<Brand?> GetByNameAsync(string name) => await dbContext
        .Brands
        .Where(x => x.Name == name)
        .FirstOrDefaultAsync();


    public async Task<PagedResult<Brand>> GetPagedAsync(
        PagedViewRequest paged, string? searchTerms)
    {
        var query = dbContext
            .Brands
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}