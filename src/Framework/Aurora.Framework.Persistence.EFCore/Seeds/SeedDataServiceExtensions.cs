using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Aurora.Framework.Persistence.EFCore;

public static class SeedDataServiceExtensions
{
    public static void SeedData<TContext>(this WebApplication application)
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

        var types = typeof(TContext).Assembly.GetTypes()
            .Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(ISeedDataService<TContext>)))
            .ToList();

        types.ForEach(x =>
        {
            var instance = Activator.CreateInstance(x);
            var seedMethod = x?.GetMethod("Seed");
            seedMethod?.Invoke(instance, [context]);
        });

        context.SaveChanges();
    }

    public static TEntity? GetFromFile<TEntity, TContext>(this TContext context, string path)
        where TEntity : class
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(path);

        string jsonContent = File.ReadAllText(path);
        return JsonSerializer.Deserialize<TEntity>(jsonContent);
    }
}