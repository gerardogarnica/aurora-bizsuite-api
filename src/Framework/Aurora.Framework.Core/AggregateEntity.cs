using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.Framework;

public class AggregateEntity<TId> : BaseEntity<TId>
{
    private readonly List<BaseEvent> _domainEvents = [];

    protected AggregateEntity(TId id) : base(id)
    {
    }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}