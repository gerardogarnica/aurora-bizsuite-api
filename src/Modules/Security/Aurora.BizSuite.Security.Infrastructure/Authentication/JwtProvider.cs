﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.BizSuite.Security.Infrastructure.Authentication;

internal sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private List<Claim> _claims = [];
    private readonly JwtOptions _options = options.Value;

    public IdentityToken CreateToken(UserInfo user, Guid applicationId)
    {
        // Set user claims
        SetClaims(user, applicationId);

        // Create signing credentials
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        // Create the security token
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: _claims,
            expires: DateTime.Now.AddMinutes(_options.AccessTokenLifeTime),
            signingCredentials: signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        // Returns the identity token
        return new IdentityToken(
            tokenValue,
            token.ValidTo,
            CreateRefreshToken(),
            DateTime.Now.AddDays(_options.RefreshTokenLifeTime));
    }

    private void SetClaims(UserInfo user, Guid applicationId)
    {
        _claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new("edit", user.IsEditable.ToString()),
            new("app", applicationId.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ];

        if (user.PasswordExpirationDate.HasValue)
        {
            _claims.Add(new("ped", user.PasswordExpirationDate.Value.ToShortDateString()));
        }

        if (user.Notes is not null)
        {
            _claims.Add(new("notes", user.Notes));
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