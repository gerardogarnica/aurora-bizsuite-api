using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Infrastructure;

public abstract class BaseRepository<TEntity, TId>(DbContext context)
    where TEntity : AggregateRoot<TId>
    where TId : class
{
    private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
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

        var currentPage = totalItems > 0 ? viewRequest.PageIndex : 0;

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