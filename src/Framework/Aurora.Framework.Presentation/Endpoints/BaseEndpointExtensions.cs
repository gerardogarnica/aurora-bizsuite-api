using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Aurora.Framework.Presentation.Endpoints;

public static class BaseEndpointExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        ServiceDescriptor[] serviceDescriptor = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(x => x is { IsAbstract: false, IsInterface: false } && 
                        x.IsAssignableTo(typeof(IBaseEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IBaseEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptor);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IBaseEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IBaseEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null
            ? app
            : routeGroupBuilder;

        foreach (IBaseEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}