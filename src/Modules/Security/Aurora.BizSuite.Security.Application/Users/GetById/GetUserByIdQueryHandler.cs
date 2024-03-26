using Aurora.BizSuite.Security.Application.Roles;

namespace Aurora.BizSuite.Security.Application.Users.GetById;

public class GetUserByIdQueryHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository)
    : IQueryHandler<GetUserByIdQuery, UserInfo>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Result<UserInfo>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id));

        if (user is null)
            return Result.Fail<UserInfo>(DomainErrors.UserErrors.UserNotFound(request.Id));

        // Get user roles
        var roles = await _roleRepository
            .GetByIds(user.Roles.Aggregate(new List<RoleId>(), (acc, x) => { acc.Add(x.RoleId); return acc; }));

        // Return user info
        return user.ToUserInfo(roles.Select(x => x.ToRoleInfo()).ToList());
    }
}