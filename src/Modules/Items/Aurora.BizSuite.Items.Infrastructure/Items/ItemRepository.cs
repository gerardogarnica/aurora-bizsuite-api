namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal sealed class ItemRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Item, ItemId>(dbContext), IItemRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public override async Task<Item?> GetByIdAsync(ItemId id) => await dbContext
        .Items
        .Include(x => x.Category)
        .Include(x => x.Brand)
        .Include(x => x.Units)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    public async Task<Item?> GetByCodeAsync(string code) => await dbContext
        .Items
        .Include(x => x.Category)
        .Include(x => x.Brand)
        .Include(x => x.Units)
        .Where(x => x.Code == code)
        .FirstOrDefaultAsync();

    public async Task<PagedResult<Item>> GetPagedAsync(
        PagedViewRequest paged,
        CategoryId? categoryId,
        BrandId? brandId,
        ItemType? type,
        ItemStatus? status,
        string? searchTerms)
    {
        var query = dbContext
            .Items
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .AsNoTracking()
            .AsQueryable();

        if (categoryId is not null)
        {
            query = query.Where(x => x.CategoryId == categoryId);
        }

        if (brandId is not null)
        {
            query = query.Where(x => x.BrandId == brandId);
        }

        if (type is not null)
        {
            query = query.Where(x => x.ItemType == type);
        }

        if (status is not null)
        {
            query = query.Where(x => x.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(x =>
                x.Name.Contains(searchTerms) ||
                x.Description.Contains(searchTerms) ||
                x.Tags!.Contains(searchTerms));
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}