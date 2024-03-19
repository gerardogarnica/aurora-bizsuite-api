using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.BizSuite.Security.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        // FluentValidation
        services.AddValidatorsFromAssembly(executingAssembly);

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(executingAssembly);
            config.AddValidationBehavior();
            config.AddUnitOfWorkBehavior();
            config.AddPerformanceBehavior();
            config.AddLoggingBehavior();
        });

        return services;
    }
}