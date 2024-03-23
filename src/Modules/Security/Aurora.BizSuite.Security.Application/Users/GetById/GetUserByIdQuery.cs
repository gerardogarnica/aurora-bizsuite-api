namespace Aurora.BizSuite.Security.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserInfo>;