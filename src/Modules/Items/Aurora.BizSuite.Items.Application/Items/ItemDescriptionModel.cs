namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemDescriptionModel
{
    public Guid ItemDescriptionId { get; internal set; }
    public string? Type { get; internal set; }
    public string? Description { get; internal set; }
}

internal static class ItemDescriptionModelExtensions
{
    internal static ItemDescriptionModel ToItemDescriptionModel(this ItemDescription itemDescription)
    {
        return new ItemDescriptionModel
        {
            ItemDescriptionId = itemDescription.Id,
            Type = itemDescription.Type,
            Description = itemDescription.Description
        };
    }
}