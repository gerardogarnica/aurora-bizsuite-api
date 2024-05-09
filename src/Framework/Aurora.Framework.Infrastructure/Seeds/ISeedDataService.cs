using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Infrastructure.Seeds;

public interface ISeedDataService<T> where T : DbContext
{
    void Seed(T context);
}