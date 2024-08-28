using System.Linq.Expressions;

namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal sealed class ItemRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Item, ItemId>(dbContext), IItemRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    private async Task<Item?> GetItemAsync(Expression<Func<Item, bool>> predicate) => await dbContext
        .Items
        .Include(x => x.Category)
        .Include(x => x.Brand)
        .Include(x => x.Descriptions)
        .Include(x => x.Resources)
        .Include(x => x.Units)
        .Include(x => x.RelatedItems)
        .AsSplitQuery()
        .Where(predicate)
        .FirstOrDefaultAsync();

    public override async Task<Item?> GetByIdAsync(ItemId id) => await GetItemAsync(x => x.Id == id);

    public async Task<Item?> GetByCodeAsync(string code) => await GetItemAsync(x => x.Code == code);

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