namespace Aurora.Framework;

public interface IBaseEntity
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void ClearDomainEvents();
}