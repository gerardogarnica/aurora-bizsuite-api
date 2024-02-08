using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Behavior;

public static class BehaviorsConfiguration
{
    public static MediatRServiceConfiguration AddLoggingBehavior(this MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return configuration;
    }

    public static MediatRServiceConfiguration AddPerformanceBehavior(this MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        return configuration;
    }

    public static MediatRServiceConfiguration AddUnitOfWorkBehavior(this MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        return configuration;
    }

    public static MediatRServiceConfiguration AddValidationBehavior(this MediatRServiceConfiguration configuration)
    {
        configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return configuration;
    }
}