namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class RoleRepository(SecurityContext context)
    : BaseRepository<Role, RoleId>(context), IRoleRepository
{
    private readonly SecurityContext _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public Task<PagedResult<Role>> GetPagedAsync(
        PagedViewRequest paged,
        string? searchTerms,
        bool onlyActives)
    {
        throw new NotImplementedException();
    }
}