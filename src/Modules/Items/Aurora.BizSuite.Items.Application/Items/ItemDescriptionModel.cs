namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemDescriptionModel(
    Guid Id,
    string? Type,
    string? Description);

internal static class ItemDescriptionModelExtensions
{
    internal static ItemDescriptionModel ToItemDescriptionModel(this ItemDescription itemDescription) => new(
        itemDescription.Id,
        itemDescription.Type,
        itemDescription.Description);
}