using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Aurora.Framework.Api;

public static class BaseEndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var serviceDescriptor = Assembly
            .GetCallingAssembly()
            .DefinedTypes
            .Where(x => x.IsAssignableTo(typeof(IBaseEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IBaseEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptor);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IBaseEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null
            ? app
            : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}