namespace Aurora.BizSuite.Settings.Infrastructure.Repositories;

internal abstract class BaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : class
{
    protected readonly SettingsContext DbContext;

    protected BaseRepository(SettingsContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public void Insert(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }
}