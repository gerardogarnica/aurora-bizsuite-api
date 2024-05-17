using System.Text.Json.Serialization;

namespace Aurora.BizSuite.Api.Extensions;

internal static class SerializerExtensions
{
    internal static IServiceCollection AddStringEnumConverter(this IServiceCollection services)
    {
        services.AddMvc().AddJsonOptions(a =>
        {
            a.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}