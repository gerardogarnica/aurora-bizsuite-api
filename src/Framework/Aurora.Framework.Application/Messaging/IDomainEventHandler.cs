using MediatR;

namespace Aurora.Framework.Application;

public interface IDomainEventHandler<in T>
    : INotificationHandler<T>
    where T : IDomainEvent;