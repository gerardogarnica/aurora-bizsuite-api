﻿namespace Aurora.BizSuite.Security.Application.Users;

internal static class UserInfoExtensions
{
    internal static UserInfo ToUserInfo(this User user)
    {
        return new UserInfo(
            user.Id.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PasswordExpirationDate,
            user.Notes,
            user.IsEditable);
    }
}