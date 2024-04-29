using System.Linq.Expressions;
using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class RoleRepository(
    SecurityContext context,
    IApplicationProvider applicationProvider)
    : BaseRepository<Role, RoleId>(context), IRoleRepository
{
    public IUnitOfWork UnitOfWork => context;

    private readonly ApplicationId _currentApplicationId = new(applicationProvider.GetApplicationId());

    public override async Task<Role?> GetByIdAsync(RoleId id)
    {
        Expression<Func<Role, bool>> filter = applicationProvider.IsAdminApp()
            ? x => x.Id == id
            : x => x.Id == id && x.ApplicationId == _currentApplicationId;

        return await context
            .Roles
            .IgnoreQueryFilters()
            .Include(x => x.Users)
            .Where(filter)
            .FirstOrDefaultAsync();
    }

    public async Task<Role?> GetByNameAsync(ApplicationId applicationId, string name)
    {
        return await context
            .Roles
            .IgnoreQueryFilters()
            .Where(x => x.ApplicationId == applicationId && x.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task<IList<Role>> GetByIds(IList<RoleId> ids)
    {
        Expression<Func<Role, bool>> filter = applicationProvider.IsAdminApp()
            ? x => ids.Contains(x.Id)
            : x => ids.Contains(x.Id) && x.ApplicationId == _currentApplicationId;

        return await context
            .Roles
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

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

        if (!applicationProvider.IsAdminApp())
        {
            query.Where(x => x.ApplicationId == _currentApplicationId);
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}