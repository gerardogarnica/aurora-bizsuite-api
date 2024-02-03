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

    public async Task InsertAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    protected async Task<PagedResult<T>> ToPagedResultAsync<T>(
        IQueryable<T> query,
        PagedViewRequest viewRequest)
        where T : class
    {
        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((viewRequest.PageIndex - 1) * viewRequest.PageSize)
            .Take(viewRequest.PageSize)
            .ToListAsync();

        var currentPage = totalItems > 0 ? viewRequest.PageIndex + 1 : 0;

        var totalPages = (int)Math.Ceiling(totalItems / (double)viewRequest.PageSize);

        if (totalPages < currentPage)
            throw new ArgumentOutOfRangeException("The current page cannot be greater than the total pages.");

        return new PagedResult<T>(
            items,
            totalItems,
            currentPage,
            totalPages);
    }
}