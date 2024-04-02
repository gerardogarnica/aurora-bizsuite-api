using Aurora.BizSuite.Security.Infrastructure.Authentication;
using Aurora.BizSuite.Security.Infrastructure.Cryptography;
using Aurora.Framework.Identity;
using Microsoft.Extensions.Configuration;
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

        // IUnitOfWork implementation
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SecurityContext>());

        // Repository implementations
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}