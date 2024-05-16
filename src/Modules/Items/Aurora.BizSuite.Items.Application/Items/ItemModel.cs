using System.Text.Json.Serialization;

namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemModel
{
    public Guid ItemId { get; internal set; }
    public string? Name { get; internal set; }
    public string? Description { get; internal set; }
    public Guid CategoryId { get; internal set; }
    public string? CategoryName { get; internal set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemType ItemType { get; internal set; }
    public string? AlternCode { get; internal set; }
    public string? Notes { get; internal set; }
}

internal static class ItemModelExtensions
{
    internal static ItemModel ToItemModel(
        this Item item)
    {
        return new ItemModel
        {
            ItemId = item.Id.Value,
            Name = item.Name,
            Description = item.Description,
            CategoryId = item.Category.Id.Value,
            CategoryName = item.Category.Name,
            ItemType = item.ItemType,
            AlternCode = item.AlternCode,
            Notes = item.Notes
        };
    }
}