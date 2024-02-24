﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Framework.Identity;

public interface IJwtProvider
{
    IdentityToken CreateToken(UserInfo user);
}

public sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private List<Claim> _claims = [];
    private readonly JwtOptions _options = options.Value;

    public IdentityToken CreateToken(UserInfo user)
    {
        // Set user claims
        SetClaims(user);

        // Create signing credentials
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        // Create the security token
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            _claims,
            null,
            DateTime.Now.AddMinutes(_options.AccessTokenLifeTime),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        // Returns the identity token
        return new IdentityToken(
            tokenValue,
            token.ValidTo,
            CreateRefreshToken(),
            DateTime.Now.AddDays(_options.RefreshTokenLifeTime));
    }

    private void SetClaims(UserInfo user)
    {
        _claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
        ];

        if (user.PasswordExpirationDate.HasValue)
        {
            _claims.Add(new("ped", user.PasswordExpirationDate.Value.ToShortDateString()));
        }

        foreach (var role in user.Roles)
        {
            _claims.Add(new("role", role.RoleId.ToString()));
        }
    }

    private static string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}