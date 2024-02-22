namespace Aurora.Framework.Identity;

public record IdentityToken(
    string AccessToken,
    DateTime AccessTokenExpiration,
    string RefreshToken,
    DateTime RefreshTokenExpiration);