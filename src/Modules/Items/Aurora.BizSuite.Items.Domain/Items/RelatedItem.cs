namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class RelatedItem
{
    public Guid Id { get; private set; }
    public ItemId ItemId { get; private set; }
    public ItemId RelatedItemId { get; private set; }

    private RelatedItem()
    {
        Id = Guid.NewGuid();
        ItemId = new ItemId(Guid.Empty);
        RelatedItemId = new ItemId(Guid.Empty);
    }

    internal static RelatedItem Create(ItemId itemId, ItemId relatedItemId)
    {
        return new RelatedItem
        {
            ItemId = itemId,
            RelatedItemId = relatedItemId
        };
    }
}