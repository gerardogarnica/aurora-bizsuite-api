namespace Aurora.BizSuite.Security.Application.Roles.GetById;

public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleInfo>;