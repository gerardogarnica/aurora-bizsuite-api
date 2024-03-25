namespace Aurora.BizSuite.Security.Application.Users.GetList;

public sealed record GetUserListQuery(
    PagedViewRequest PagedView,
    Guid? RoleId,
    string? SearchTerms,
    bool OnlyActives) : IQuery<PagedResult<UserInfo>>;