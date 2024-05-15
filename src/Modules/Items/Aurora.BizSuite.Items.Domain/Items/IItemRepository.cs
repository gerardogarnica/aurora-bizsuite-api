namespace Aurora.BizSuite.Items.Domain.Items;

public interface IItemRepository : IRepository<Item>
{
    Task<Item?> GetByIdAsync(ItemId id);
}