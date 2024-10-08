﻿namespace Aurora.BizSuite.Items.Application.Items;

public sealed record ItemImageModel(
    Guid Id,
    string? Uri,
    int Order);

internal static class ItemImageModelExtensions
{
    internal static ItemImageModel ToItemImageModel(this ItemResource itemResource) => new(
        itemResource.Id,
        itemResource.Uri,
        itemResource.Order);
}