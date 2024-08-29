namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemDocumentModel(
    Guid Id,
    string? Name,
    string? Uri);

internal static class ItemDocumentModelExtensions
{
    internal static ItemDocumentModel ToItemDocumentModel(this ItemResource itemResource)
    {
        return new ItemDocumentModel(
            itemResource.Id,
            itemResource.Name,
            itemResource.Uri);
    }
}