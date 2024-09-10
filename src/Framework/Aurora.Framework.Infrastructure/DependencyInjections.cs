using Aurora.Framework.Application;
using Aurora.Framework.Infrastructure.Interceptors;
using Aurora.Framework.Infrastructure.Time;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aurora.Framework.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddCommonInfrastructureServices(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        // EF Core interceptors
        services.TryAddSingleton<PublishDomainEventsInterceptor>();
        services.TryAddSingleton<AuditableEntitiesInterceptor>();
        services.TryAddSingleton<SoftDeletableEntitiesInterceptor>();

        // MassTransit event bus
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configure =>
        {
            foreach (var moduleConsumer in moduleConfigureConsumers)
            {
                moduleConsumer(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, memoryConfigure) =>
            {
                memoryConfigure.ConfigureEndpoints(context);
            });
        });

        // DateTime services
        services.TryAddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}