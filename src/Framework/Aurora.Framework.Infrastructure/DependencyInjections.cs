using Aurora.Framework.Application;
using Aurora.Framework.Infrastructure.Interceptors;
using Aurora.Framework.Infrastructure.Time;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;

namespace Aurora.Framework.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddCommonInfrastructureServices(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddEntityFrameworkCoreInterceptors();
        services.AddMassTransitConfiguration(moduleConfigureConsumers);
        services.AddQuartzConfiguration();
        services.AddDateTimeServices();

        return services;
    }

    private static IServiceCollection AddEntityFrameworkCoreInterceptors(this IServiceCollection services)
    {
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();
        services.TryAddSingleton<AuditableEntitiesInterceptor>();
        services.TryAddSingleton<SoftDeletableEntitiesInterceptor>();

        return services;
    }

    private static IServiceCollection AddMassTransitConfiguration(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
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

        return services;
    }

    private static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }

    private static IServiceCollection AddDateTimeServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}