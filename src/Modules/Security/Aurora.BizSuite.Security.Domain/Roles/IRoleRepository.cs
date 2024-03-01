namespace Aurora.BizSuite.Security.Domain.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> InsertAsync(Role role);
    void Update(Role role);
    Task<Role?> GetByIdAsync(RoleId id);
    Task<PagedResult<Role>> GetPagedAsync(PagedViewRequest paged, string? searchTerms, bool onlyActives);
}