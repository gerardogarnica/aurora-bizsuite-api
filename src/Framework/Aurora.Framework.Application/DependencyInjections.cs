using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.Framework.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddCommonApplicationServices(
        this IServiceCollection services,
        Assembly[] assemblies)
    {
        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // FluentValidation
        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }
}