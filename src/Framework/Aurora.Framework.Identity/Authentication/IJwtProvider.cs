namespace Aurora.Framework.Identity;

public interface IJwtProvider
{
    IdentityToken CreateToken(UserInfo user, Guid applicationId);
}