using Aurora.BizSuite.Security.Application.Roles;
using Aurora.BizSuite.Security.Application.Users;
using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Identity.Login;

public class LoginCommandHandler(
    IJwtProvider jwtProvider,
    IPasswordProvider passwordProvider,
    IApplicationRepository applicationRepository,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ISessionRepository sessionRepository)
    : ICommandHandler<LoginCommand, IdentityToken>
{
    public async Task<Result<IdentityToken>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // Get application
        var applicationId = new ApplicationId(request.Application);
        var application = await applicationRepository.GetByIdAsync(applicationId);
        if (application is null)
            return Result.Fail<IdentityToken>(DomainErrors.ApplicationErrors.ApplicationNotFound(request.Application));

        // Get user by email
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        if (user.Status is not UserStatusType.Active)
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        var passwordResult = Password.Create(request.Password);
        if (!passwordResult.IsSuccessful)
            return Result.Fail<IdentityToken>(passwordResult.Error);

        if (!user.PasswordMatches(passwordProvider, passwordResult.Value))
            return Result.Fail<IdentityToken>(DomainErrors.UserErrors.InvalidCredentials);

        // Get user roles
        var roles = await roleRepository
            .GetByIds(user.Roles.Aggregate(new List<RoleId>(), (acc, x) => { acc.Add(x.RoleId); return acc; }));

        // Create user info
        var userInfo = user.ToUserInfo(roles.Select(x => x.ToRoleInfo()).ToList());

        // Generate identity token
        var identityToken = jwtProvider.CreateToken(userInfo);

        // Creates user session
        var session = UserSession
            .Create(
                user.Id,
                request.Application.ToString(),
                identityToken)
            .Value;

        await sessionRepository.InsertAsync(session);

        // Returns the identity token
        return identityToken;
    }
}