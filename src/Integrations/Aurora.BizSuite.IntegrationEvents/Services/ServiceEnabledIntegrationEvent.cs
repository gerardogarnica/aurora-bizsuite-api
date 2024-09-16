namespace Aurora.BizSuite.IntegrationEvents.Services;

public sealed class ServiceEnabledIntegrationEvent(
    Guid id,
    DateTime occurredOnUtc,
    Guid serviceId,
    string code,
    string name,
    string description)
    : IntegrationEvent(id, occurredOnUtc)
{
    public Guid ServiceId { get; init; } = serviceId;
    public string Code { get; init; } = code;
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
}