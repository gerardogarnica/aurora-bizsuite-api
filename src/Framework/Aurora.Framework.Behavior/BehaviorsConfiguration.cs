using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Behavior;

public static class BehaviorsConfiguration
{
    /*
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
    */

    public static MediatRServiceConfiguration AddValidationBehavior(this MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return configuration;
    }
}