namespace Aurora.BizSuite.Security.Application.Identity.Login;

public class LoginCommandHandler(
    IJwtProvider jwtProvider,
    IPasswordProvider passwordProvider,
    IUserRepository userRepository,
    ISessionRepository sessionRepository)
    : ICommandHandler<LoginCommand, IdentityToken>
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IUserRepository _userRepository = userRepository;
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

        var userInfo = new UserInfo(
            user.Id.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PasswordExpirationDate,
            user.Notes,
            user.IsEditable);

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