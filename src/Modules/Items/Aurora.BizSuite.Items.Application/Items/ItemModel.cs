using Aurora.BizSuite.Items.Application.Brands;
using Aurora.BizSuite.Items.Application.Categories;
using System.Text.Json.Serialization;

namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemModel
{
    public Guid Id { get; internal set; }
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
    public List<ItemImageModel> Images { get; internal set; } = [];
    public List<ItemDocumentModel> Documents { get; internal set; } = [];
}

internal static class ItemModelExtensions
{
    internal static ItemModel ToItemModel(this Item item)
    {
        return new ItemModel
        {
            Id = item.Id.Value,
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
            Descriptions = item.Descriptions.Select(x => x.ToItemDescriptionModel()).ToList(),
            Images = [.. item.Resources.Where(x => x.Type == ItemConstants.ImageResourceTypeName).Select(x => x.ToItemImageModel()).OrderBy(x => x.Order)],
            Documents = [.. item.Resources.Where(x => x.Type == ItemConstants.DocumentResourceTypeName).Select(x => x.ToItemDocumentModel()).OrderBy(x => x.Name)]
        };
    }
}