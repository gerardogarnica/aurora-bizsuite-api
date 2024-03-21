namespace Aurora.BizSuite.Security.Application.Identity.Login;

public record UserCredentials(string Email, string Password);

public sealed record LoginCommand : UserCredentials, ICommand<IdentityToken>
{
    public LoginCommand(string Email, string Password)
        : base(Email, Password) { }

    public required string Application { get; init; }
}