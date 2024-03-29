﻿namespace Aurora.BizSuite.Security.Domain.Users;

public record UserId(Guid Value);

public record UserRoleId(int Value);

public enum UserStatusType : byte
{
    Pending = 0,
    Active = 1,
    Inactive = 2
}