﻿using Aurora.Framework.Application.Time;
using Aurora.Framework.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aurora.Framework.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddCommonInfrastructureServices(
        this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}