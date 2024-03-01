namespace Aurora.BizSuite.Security.Domain.Users;

public record UserId(Guid Value);

public enum UserStatusType
{
    Pending = 0,
    Active = 1,
    Inactive = 2
}