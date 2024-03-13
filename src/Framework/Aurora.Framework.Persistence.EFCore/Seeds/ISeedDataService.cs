using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Persistence.EFCore;

public interface ISeedDataService<T> where T : DbContext
{
    void Seed(T context);
}