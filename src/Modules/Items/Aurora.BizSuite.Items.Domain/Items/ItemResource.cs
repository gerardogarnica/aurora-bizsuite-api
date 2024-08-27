namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemResource
{
    public Guid Id { get; private set; }
    public ItemId ItemId { get; private set; }
    public string Type { get; private set; }
    public string Name { get; private set; }
    public string Uri { get; private set; }
    public int Order { get; private set; }

    private ItemResource()
    {
        Id = Guid.NewGuid();
        ItemId = new ItemId(Guid.Empty);
        Type = string.Empty;
        Name = string.Empty;
        Uri = string.Empty;
    }

    internal static ItemResource Create(
        ItemId itemId,
        string type,
        string name,
        string uri,
        int order)
    {
        return new ItemResource
        {
            ItemId = itemId,
            Type = type,
            Name = name,
            Uri = uri,
            Order = order
        };
    }

    internal void UpOrder() => Order--;

    internal void DownOrder() => Order++;
}