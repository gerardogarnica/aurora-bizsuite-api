namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class UserRepository(SecurityContext context)
    : BaseRepository<User, UserId>(context), IUserRepository
{
    public IUnitOfWork UnitOfWork => context;

    public override async Task<User?> GetByIdAsync(UserId id) => await context
        .Users
        .Include(x => x.Roles)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    public async Task<User?> GetByEmailAsync(string email) => await context
        .Users
        .Include(x => x.Roles)
        .Where(x => x.Email == email)
        .FirstOrDefaultAsync();

    public async Task<PagedResult<User>> GetPagedAsync(
        PagedViewRequest paged,
        string? searchTerms,
        bool onlyActives)
    {
        var query = context
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

    public async Task<PagedResult<User>> GetPagedAsync(
        PagedViewRequest paged,
        RoleId roleId,
        string? searchTerms,
        bool onlyActives)
    {
        var userRoles = context
            .UserRoles
            .Where(x => x.RoleId == roleId)
            .Select(x => x.UserId)
            .ToList();

        var query = context
            .Users
            .Where(x => userRoles.Contains(x.Id))
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
}