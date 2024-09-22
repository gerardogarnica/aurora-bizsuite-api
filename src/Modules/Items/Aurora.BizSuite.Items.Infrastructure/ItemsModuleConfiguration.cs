using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.BizSuite.Items.Infrastructure;

public static class ItemsModuleConfiguration
{
    public static IServiceCollection AddItemsModuleServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection
        var connectionString = configuration.GetConnectionString("ItemsDataConnection");

        services.AddDbContext<ItemsDbContext>((sp, options) =>
        {
            options
                .UseSqlServer(
                    connectionString,
                    x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ItemsDbContext.DEFAULT_SCHEMA))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditableEntitiesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<SoftDeletableEntitiesInterceptor>());
        });

        // IUnitOfWork implementation
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ItemsDbContext>());

        // Repository implementations
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        // Inbox pattern implementation
        services.Configure<InboxOptions>(configuration.GetSection("Items:Inbox"));
        services.ConfigureOptions<ConfigureItemsInboxJob>();

        // Outbox pattern implementation
        services.Configure<OutboxOptions>(configuration.GetSection("Items:Outbox"));
        services.ConfigureOptions<ConfigureItemsOutboxJob>();

        // Integration event handler implementations

        return services;
    }
}