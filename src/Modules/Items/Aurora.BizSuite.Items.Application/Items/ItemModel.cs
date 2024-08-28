using Aurora.BizSuite.Items.Application.Brands;
using Aurora.BizSuite.Items.Application.Categories;
using System.Text.Json.Serialization;

namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemModel
{
    public Guid ItemId { get; internal set; }
    public string? Code { get; internal set; }
    public string? Name { get; internal set; }
    public string? Description { get; internal set; }
    public CategoryModel? Category { get; internal set; }
    public BrandModel? Brand { get; internal set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemType ItemType { get; internal set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemStatus Status { get; internal set; }
    public string? AlternativeCode { get; internal set; }
    public string? Notes { get; internal set; }
    public List<string> Tags { get; internal set; } = [];
    public List<ItemUnitModel> Units { get; internal set; } = [];
    public List<ItemDescriptionModel> Descriptions { get; internal set; } = [];
}

internal static class ItemModelExtensions
{
    internal static ItemModel ToItemModel(this Item item)
    {
        return new ItemModel
        {
            ItemId = item.Id.Value,
            Code = item.Code,
            Name = item.Name,
            Description = item.Description,
            Category = item.Category.ToCategoryModel(),
            Brand = item.Brand.ToBrandModel(),
            ItemType = item.ItemType,
            Status = item.Status,
            AlternativeCode = item.AlternativeCode,
            Notes = item.Notes,
            Tags = item.Tags == null ? [] : [.. item.Tags.Split(";")],
            Units = item.Units.Select(x => x.ToItemUnitModel()).ToList(),
            Descriptions = item.Descriptions.Select(x => x.ToItemDescriptionModel()).ToList()
        };
    }
}