using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.BizSuite.Items.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddItemsModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Common services
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddCommonApplicationServices([Application.AssemblyReference.Assembly]);
        services.AddCommonInfrastructureServices();

        // Database connection
        var connectionString = configuration.GetConnectionString("ItemsDataConnection");

        services.AddDbContext<ItemsDbContext>((sp, options) =>
        {
            options
                .UseSqlServer(
                    connectionString,
                    x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ItemsDbContext.DEFAULT_SCHEMA))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditableEntitiesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeletableEntitiesInterceptor>());
        });

        // IUnitOfWork implementation
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ItemsDbContext>());

        // Repository implementations
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        return services;
    }
}