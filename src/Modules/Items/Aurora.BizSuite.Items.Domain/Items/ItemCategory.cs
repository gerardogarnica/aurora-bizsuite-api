using Aurora.BizSuite.Items.Domain.Categories;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemCategory
{
    public ItemId ItemId { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public Category Category { get; init; } = null!;

    private ItemCategory()
    {
        ItemId = new ItemId(Guid.Empty);
        CategoryId = new CategoryId(Guid.Empty);
    }

    internal static ItemCategory Create(ItemId itemId, CategoryId categoryId)
    {
        return new ItemCategory
        {
            ItemId = itemId,
            CategoryId = categoryId
        };
    }
}