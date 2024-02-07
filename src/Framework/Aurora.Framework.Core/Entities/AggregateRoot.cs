using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.Framework;

public interface IAggregateRoot
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void ClearDomainEvents();
    void RemoveDomainEvent(BaseEvent domainEvent);
}

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
{
    protected AggregateRoot() { }

    protected AggregateRoot(TId id)
        : base(id) { }


    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
}