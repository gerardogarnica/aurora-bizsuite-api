namespace Aurora.Framework.Identity;

public interface IPasswordProvider
{
    string HashPassword(string password);
    bool VerifyPassword(string userPassword, string providedPassword);
}