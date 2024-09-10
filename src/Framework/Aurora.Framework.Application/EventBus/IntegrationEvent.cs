﻿namespace Aurora.Framework.Application;

public abstract class IntegrationEvent(Guid id, DateTime occurredOnUtc)
    : IIntegrationEvent
{
    public Guid Id { get; init; } = id;

    public DateTime OccurredOnUtc { get; init; } = occurredOnUtc;
}