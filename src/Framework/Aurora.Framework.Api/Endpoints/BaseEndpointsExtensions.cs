using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Aurora.Framework.Api;

public static class BaseEndpointsExtensions
{
    public static void AddEndpoints(this WebApplication app)
    {
        Assembly.GetCallingAssembly().GetTypes()
            .Where(x => x.GetInterfaces().Contains(typeof(IBaseEndpoints)))
            .ToList()
            .ForEach(x =>
            {
                var instance = Activator.CreateInstance(x);
                var addRoutes = x?.GetMethod("AddRoutes");
                addRoutes?.Invoke(instance, new object[] { app });
            });
    }
}