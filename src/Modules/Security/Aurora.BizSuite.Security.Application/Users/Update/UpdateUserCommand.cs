namespace Aurora.BizSuite.Security.Application.Users.Update;

public sealed record UpdateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string? Notes) : ICommand;