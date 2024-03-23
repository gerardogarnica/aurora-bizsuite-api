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

    public async Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, string? searchTerms, bool onlyActives)
    {
        var query = _context
            .Users
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(
                x => x.FirstName.Contains(searchTerms) ||
                x.LastName.Contains(searchTerms) ||
                x.Email.Contains(searchTerms));
        }

        if (onlyActives)
        {
            query = query.Where(x => x.Status == UserStatusType.Active);
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.FirstName), paged);
    }

    public Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, Guid roleId, string? searchTerms, bool onlyActives)
    {
        throw new NotImplementedException();
    }
}