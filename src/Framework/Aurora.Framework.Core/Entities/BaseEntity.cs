using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.Framework;

public abstract class BaseEntity<TId> : IBaseEntity
{
    protected BaseEntity()
    {
        Id = default!;
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }

    public TId Id { get; init; }

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}