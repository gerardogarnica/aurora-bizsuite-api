namespace Aurora.Framework.Identity;

public class JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public int AccessTokenLifeTime { get; init; }
    public int RefreshTokenLifeTime { get; init; }
}