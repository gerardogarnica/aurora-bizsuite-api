using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class RoleRepository(SecurityContext context)
    : BaseRepository<Role, RoleId>(context), IRoleRepository
{
    public IUnitOfWork UnitOfWork => context;

    public override async Task<Role?> GetByIdAsync(RoleId id) => await context
        .Roles
        .Include(x => x.Users)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

    public async Task<IList<Role>> GetByIds(IList<RoleId> ids) => await context
        .Roles
        .Where(x => ids.Contains(x.Id))
        .ToListAsync();

    public async Task<IList<Role>> GetByIds(IList<RoleId> ids, ApplicationId applicationId) => await context
        .Roles
        .IgnoreQueryFilters()
        .Where(x => ids.Contains(x.Id))
        .Where(x => x.ApplicationId == applicationId)
        .ToListAsync();

    public async Task<PagedResult<Role>> GetPagedAsync(
        PagedViewRequest paged,
        string? searchTerms,
        bool onlyActives)
    {
        var query = onlyActives
            ? context.Roles.AsQueryable()
            : context.Roles.IgnoreQueryFilters().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerms) && searchTerms.Length >= 3)
        {
            query = query.Where(
                x => x.Name.Contains(searchTerms) ||
                x.Description.Contains(searchTerms));
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}