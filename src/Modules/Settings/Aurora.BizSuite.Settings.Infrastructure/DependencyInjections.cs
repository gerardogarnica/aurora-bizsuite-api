using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.BizSuite.Settings.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Connection string
        services.AddDbContext<SettingsContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SettingsDataConnection"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", SettingsContext.DEFAULT_SCHEMA));
        });

        // IUnitOfWork implementation
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SettingsContext>());

        // Repositories implementations
        services.AddScoped<IOptionRepository, OptionRepository>();

        return services;
    }
}