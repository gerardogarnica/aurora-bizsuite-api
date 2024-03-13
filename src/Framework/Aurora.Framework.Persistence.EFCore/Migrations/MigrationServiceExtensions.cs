using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Persistence.EFCore;

public static class MigrationServiceExtensions
{
    public static void MigrateDatabase<TContext>(this WebApplication application)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(application);
        ArgumentNullException.ThrowIfNull(application.Services);

        var context = application
            .Services
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<TContext>();

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.Database.Migrate();
    }
}