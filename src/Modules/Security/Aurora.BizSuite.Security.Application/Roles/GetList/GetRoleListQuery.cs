namespace Aurora.BizSuite.Security.Application.Roles.GetList;

public sealed record GetRoleListQuery(
    PagedViewRequest PagedView,
    string? SearchTerms,
    bool OnlyActives) : IQuery<PagedResult<RoleInfo>>;