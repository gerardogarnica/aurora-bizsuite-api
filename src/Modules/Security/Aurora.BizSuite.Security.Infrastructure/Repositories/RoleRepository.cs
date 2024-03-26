namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class RoleRepository(SecurityContext context)
    : BaseRepository<Role, RoleId>(context), IRoleRepository
{
    private readonly SecurityContext _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IList<Role>> GetByIds(IList<RoleId> ids)
    {
        return await _context
            .Roles
            .Where(x => ids.Contains(x.Id) && x.IsActive)
            .ToListAsync();
    }

    public Task<PagedResult<Role>> GetPagedAsync(
        PagedViewRequest paged,
        string? searchTerms,
        bool onlyActives)
    {
        throw new NotImplementedException();
    }
}