namespace Aurora.BizSuite.Items.Domain.Items;

internal class ItemUpdatedDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}