namespace Aurora.BizSuite.Security.Application.Users.GetList;

public class GetUserListQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserListQuery, PagedResult<UserInfo>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<PagedResult<UserInfo>>> Handle(
        GetUserListQuery request,
        CancellationToken cancellationToken)
    {
        // Get paged users
        var users = request.RoleId.HasValue
            ? await _userRepository.GetPagedAsync(
                request.PagedView,
                new RoleId(request.RoleId.Value),
                request.SearchTerms,
                request.OnlyActives)
            : await _userRepository.GetPagedAsync(
                request.PagedView,
                request.SearchTerms,
                request.OnlyActives);

        // Return paged result
        return new PagedResult<UserInfo>(
            users.Items.Select(x => x.ToUserInfo([])).ToList(),
            users.TotalItems,
            users.CurrentPage,
            users.TotalPages);
    }
}