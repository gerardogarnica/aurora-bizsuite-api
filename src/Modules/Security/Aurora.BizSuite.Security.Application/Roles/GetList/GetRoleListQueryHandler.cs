namespace Aurora.BizSuite.Security.Application.Roles.GetList;

public class GetRoleListQueryHandler(
    IRoleRepository roleRepository)
    : IQueryHandler<GetRoleListQuery, PagedResult<RoleInfo>>
{
    public async Task<Result<PagedResult<RoleInfo>>> Handle(
        GetRoleListQuery request,
        CancellationToken cancellationToken)
    {
        // Get paged roles
        var roles = await roleRepository.GetPagedAsync(
            request.PagedView,
            request.SearchTerms,
            request.OnlyActives);

        // Return paged result
        return new PagedResult<RoleInfo>(
            roles.Items.Select(x => x.ToRoleInfo()).ToList(),
            roles.TotalItems,
            roles.CurrentPage,
            roles.TotalPages);
    }
}