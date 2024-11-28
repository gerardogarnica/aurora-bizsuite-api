using System.Text.Json.Serialization;

namespace Aurora.BizSuite.Items.Application.Brands;

public sealed record BrandItemsByStatusModel(
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    ItemStatus ItemStatus,
    int ItemCount);

internal static class BrandItemsByStatusModelExtensions
{
    internal static List<BrandItemsByStatusModel> ToBrandItemsByStatusModel(this Brand brand)
    {
        return brand.Items
            .GroupBy(item => item.Status)
            .Select(group => new BrandItemsByStatusModel(group.Key, group.Count()))
            .ToList();
    }
}