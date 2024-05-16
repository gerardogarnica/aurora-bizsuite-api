namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal sealed class ItemRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Item, ItemId>(dbContext), IItemRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public override async Task<Item?> GetByIdAsync(ItemId id) => await dbContext
        .Items
        .Include(x => x.Category)
        .Include(x => x.MainUnit)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();
}