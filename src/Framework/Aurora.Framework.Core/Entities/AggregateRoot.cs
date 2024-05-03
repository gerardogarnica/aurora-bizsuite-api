using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.Framework;

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
    void RemoveDomainEvent(DomainEvent domainEvent);
}

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
{
    protected AggregateRoot() { }

    protected AggregateRoot(TId id)
        : base(id) { }


    private readonly List<DomainEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
}