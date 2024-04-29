using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Domain.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task InsertAsync(Role role);
    void Update(Role role);
    Task<Role?> GetByIdAsync(RoleId id);
    Task<Role?> GetByNameAsync(ApplicationId applicationId, string name);
    Task<IList<Role>> GetByIds(IList<RoleId> ids);
    Task<IList<Role>> GetByIds(IList<RoleId> ids, ApplicationId applicationId);
    Task<PagedResult<Role>> GetPagedAsync(PagedViewRequest paged, string? searchTerms, bool onlyActives);
}