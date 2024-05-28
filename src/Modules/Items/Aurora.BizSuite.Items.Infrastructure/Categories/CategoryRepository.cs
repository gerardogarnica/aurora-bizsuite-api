namespace Aurora.BizSuite.Items.Infrastructure.Categories;

internal sealed class CategoryRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Category, CategoryId>(dbContext), ICategoryRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public override async Task<Category?> GetByIdAsync(CategoryId id) => await dbContext
        .Categories
        .Include(x => x.Childs)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    public async Task<IReadOnlyCollection<Category>> GetListAsync(
        Guid? parentId,
        string? searchTerms)
    {
        var query = dbContext
            .Categories
            .AsNoTracking()
            .AsQueryable();

        if (parentId.HasValue)
        {
            query = query.Where(x => x.ParentId == new CategoryId(parentId.Value));
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