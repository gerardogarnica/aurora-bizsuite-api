namespace Aurora.BizSuite.Security.Application.Users.GetList;

public sealed record GetUserListQuery(
    PagedViewRequest PagedView,
    string? SearchTerms,
    bool OnlyActives) : IQuery<PagedResult<UserInfo>>;