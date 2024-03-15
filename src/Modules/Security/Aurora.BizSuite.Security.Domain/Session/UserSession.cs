using Aurora.BizSuite.Security.Domain.Users;

namespace Aurora.BizSuite.Security.Domain.Session;

public class UserSession : AggregateRoot<UserSessionId>
{
    public UserId UserId { get; private set; }
    public string Application { get; private set; }
    public string AccessToken { get; private set; }
    public DateTime AccessTokenExpiration { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiration { get; private set; }
    public DateTime BeginSessionDate { get; private set; }
    public DateTime? EndSessionDate { get; private set; }

    protected UserSession()
        : base(new UserSessionId(Guid.NewGuid()))
    {
        UserId = new UserId(Guid.NewGuid());
        Application = string.Empty;
        AccessToken = string.Empty;
        AccessTokenExpiration = DateTime.UtcNow;
        RefreshToken = string.Empty;
        RefreshTokenExpiration = DateTime.UtcNow;
        BeginSessionDate = DateTime.UtcNow;
    }

    private UserSession(
        UserId userId,
        string application,
        string accessToken,
        DateTime accessTokenExpiration,
        string refreshToken,
        DateTime refreshTokenExpiration)
        : base(new UserSessionId(Guid.NewGuid()))
    {
        UserId = userId;
        Application = application;
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
        BeginSessionDate = DateTime.UtcNow;
    }

    public static Result<UserSession> Create(
        UserId userId,
        string application,
        IdentityToken identityToken)
    {
        return new UserSession(
            userId,
            application,
            identityToken.AccessToken,
            identityToken.AccessTokenExpiration,
            identityToken.RefreshToken,
            identityToken.RefreshTokenExpiration);
    }
}