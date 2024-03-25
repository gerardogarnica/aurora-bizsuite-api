namespace Aurora.BizSuite.Security.Application.Users.UpdateStatus;

public sealed record UpdateUserStatusCommand(
    Guid Id,
    bool IsActive) : ICommand;