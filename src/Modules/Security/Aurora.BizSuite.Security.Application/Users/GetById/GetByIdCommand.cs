namespace Aurora.BizSuite.Security.Application.Users.GetById;

public sealed record GetByIdCommand(Guid Id) : IQuery<UserInfo>;