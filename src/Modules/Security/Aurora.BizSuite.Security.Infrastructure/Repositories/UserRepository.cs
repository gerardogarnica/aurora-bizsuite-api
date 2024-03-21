namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class UserRepository : BaseRepository<User, UserId>, IUserRepository
{
    private readonly SecurityContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context
            .Users
            .Include(x => x.Roles)
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();
    }

    public Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, string? searchTerms, bool onlyActives)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, Guid roleId, string? searchTerms, bool onlyActives)
    {
        throw new NotImplementedException();
    }
}