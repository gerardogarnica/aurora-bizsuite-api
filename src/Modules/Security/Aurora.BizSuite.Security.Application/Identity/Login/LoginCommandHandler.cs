using Aurora.BizSuite.Security.Application.Roles;
using Aurora.BizSuite.Security.Application.Users;

namespace Aurora.BizSuite.Security.Application.Identity.Login;

public class LoginCommandHandler(
    IJwtProvider jwtProvider,
    IPasswordProvider passwordProvider,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ISessionRepository sessionRepository)
    : ICommandHandler<LoginCommand, IdentityToken>
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ISessionRepository _sessionRepository = sessionRepository;

    public async Task<Result<IdentityToken>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // Get user by email
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        if (user.Status is not UserStatusType.Active)
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        var passwordResult = Password.Create(request.Password);
        if (!passwordResult.IsSuccessful)
            return Result.Fail<IdentityToken>(passwordResult.Error);

        if (!user.PasswordMatches(_passwordProvider, passwordResult.Value))
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        // Get user roles
        var roles = await _roleRepository
            .GetByIds(user.Roles.Aggregate(new List<RoleId>(), (acc, x) => { acc.Add(x.RoleId); return acc; }));

        // Create user info
        var userInfo = user.ToUserInfo(roles.Select(x => x.ToRoleInfo()).ToList());

        // Generate identity token
        var identityToken = _jwtProvider.CreateToken(userInfo);

        // Creates user session
        var session = UserSession
            .Create(user.Id, request.Application, identityToken)
            .Value;

        await _sessionRepository.InsertAsync(session);

        // Returns the identity token
        return identityToken;
    }
}