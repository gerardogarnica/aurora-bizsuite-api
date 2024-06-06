namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class ItemActivatedDomainEvent(Guid itemId)
    : DomainEvent
{
    public Guid ItemId { get; init; } = itemId;
}