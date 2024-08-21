namespace Aurora.BizSuite.Items.Infrastructure.Categories;

internal sealed class CategoryRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Category, CategoryId>(dbContext), ICategoryRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public override async Task<Category?> GetByIdAsync(CategoryId id) => await dbContext
        .Categories
        .Include(x => x.Childs)
        .AsSplitQuery()
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    public async Task<IReadOnlyCollection<Category>> GetListAsync(
        CategoryId? parentId,
        string? searchTerms)
    {
        var query = dbContext
            .Categories
            .Include(x => x.Childs)
            .AsNoTracking()
            .AsQueryable();

        if (parentId is not null)
        {
            query = query.Where(x => x.ParentId == parentId);
        }
        else
        {
            query = query.Where(x => x.Level == 1);
        }

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(x => x.Name.Contains(searchTerms));
        }

        return await query.OrderBy(x => x.Name).ToListAsync();
    }
}