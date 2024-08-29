namespace Aurora.Framework.Application;

public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}