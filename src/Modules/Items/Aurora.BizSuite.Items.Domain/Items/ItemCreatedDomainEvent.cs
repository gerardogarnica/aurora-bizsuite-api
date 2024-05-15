namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemCreatedDomainEvent(
    Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}