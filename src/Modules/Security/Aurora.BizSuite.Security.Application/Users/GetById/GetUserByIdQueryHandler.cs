namespace Aurora.BizSuite.Security.Application.Users.GetById;

public class GetUserByIdQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, UserInfo>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<UserInfo>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id));

        if (user is null)
            return Result.Fail<UserInfo>(DomainErrors.UserErrors.UserNotFound(request.Id));

        // TODO: Get user roles

        // Return user info
        return user.ToUserInfo();
    }
}