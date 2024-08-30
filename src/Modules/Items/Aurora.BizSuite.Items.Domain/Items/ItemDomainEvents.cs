namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemEnabledDomainEvent(Guid itemId) : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

public sealed class ItemCreatedDomainEvent(Guid itemId) : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

public sealed class ItemDisabledDomainEvent(Guid itemId) : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}

public sealed class ItemUpdatedDomainEvent(Guid itemId) : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}