namespace Aurora.BizSuite.Security.Application.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string? Notes,
    bool IsEditable) : ICommand<Guid>;