﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.BizSuite.Security.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Interceptors
        services.AddSingleton<AuditableEntitiesInterceptor>();

        // Connection string
        services.AddDbContext<SecurityContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SecurityDataConnection"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", SecurityContext.DEFAULT_SCHEMA))
                .AddInterceptors(sp.GetService<AuditableEntitiesInterceptor>()!)
                .AddInterceptors(sp.GetService<SoftDeletableEntitiesInterceptor>()!);
        });

        // Authentication services
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordProvider, PasswordProvider>();
        services.AddScoped<IApplicationProvider, ApplicationProvider>();

        // IUnitOfWork implementation
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SecurityContext>());

        // Repository implementations
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}