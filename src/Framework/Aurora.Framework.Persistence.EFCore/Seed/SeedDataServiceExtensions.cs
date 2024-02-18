using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Persistence.EFCore;

public static class SeedDataServiceExtensions
{
    public static void SeedData<TContext>(this IServiceProvider service)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(service);

        var context = service.CreateScope().ServiceProvider.GetRequiredService<TContext>();
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
}