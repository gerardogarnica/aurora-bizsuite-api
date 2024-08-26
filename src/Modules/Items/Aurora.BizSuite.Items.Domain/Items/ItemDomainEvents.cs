﻿namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemActivatedDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

public sealed class ItemCreatedDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

public sealed class ItemDisabledDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

internal class ItemUpdatedDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}