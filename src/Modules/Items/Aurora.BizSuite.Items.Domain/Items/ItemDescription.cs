namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemDescription
{
    public Guid Id { get; private set; }
    public ItemId ItemId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }

    private ItemDescription()
    {
        Id = Guid.NewGuid();
        ItemId = new ItemId(Guid.Empty);
        Type = string.Empty;
        Description = string.Empty;
    }

    internal static ItemDescription Create(
        ItemId itemId,
        string type,
        string description)
    {
        return new ItemDescription
        {
            ItemId = itemId,
            Type = type,
            Description = description
        };
    }

    internal void Update(string description)
    {
        Description = description;
    }
}