namespace Aurora.BizSuite.Items.Application.Brands;

public sealed record BrandModel(
    Guid Id,
    string Name,
    string? LogoUri,
    string? Notes,
    List<BrandItemsByStatusModel> ItemsByStatus);

internal static class BrandModelExtensions
{
    internal static BrandModel ToBrandModel(this Brand brand) => new(
        brand.Id.Value,
        brand.Name,
        brand.LogoUri,
        brand.Notes,
        brand.ToBrandItemsByStatusModel());
}