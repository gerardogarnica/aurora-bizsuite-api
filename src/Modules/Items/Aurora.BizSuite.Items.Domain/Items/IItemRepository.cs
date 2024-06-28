using Aurora.BizSuite.Items.Domain.Categories;

namespace Aurora.BizSuite.Items.Domain.Items;

public interface IItemRepository : IRepository<Item>
{
    Task<Item?> GetByIdAsync(ItemId id);
    Task<Item?> GetByCodeAsync(string code);
    Task<PagedResult<Item>> GetPagedAsync(PagedViewRequest paged, CategoryId? categoryId, ItemType? type, ItemStatus? status, string? searchTerms);
    Task InsertAsync(Item item);
    void Update(Item item);
}