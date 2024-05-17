using Aurora.BizSuite.Items.Infrastructure.Database;
using Aurora.Framework.Infrastructure.Migrations;
using Aurora.Framework.Infrastructure.Seeds;

namespace Aurora.BizSuite.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this WebApplication app)
    {
        app.MigrateDatabase<ItemsDbContext>();

        app.SeedData<ItemsDbContext>();
    }
}