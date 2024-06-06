namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemDisabledDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}