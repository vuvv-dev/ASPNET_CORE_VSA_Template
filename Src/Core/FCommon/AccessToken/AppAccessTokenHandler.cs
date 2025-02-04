using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FCommon.Constants;
using Microsoft.IdentityModel.Tokens;

namespace FCommon.AccessToken;

public sealed class AppAccessTokenHandler : IAppAccessTokenHandler
{
    private readonly TokenValidationParameters _validationParameters;

    public AppAccessTokenHandler(TokenValidationParameters validationParameters)
    {
        _validationParameters = validationParameters;
    }

    public string GenerateJWS(IEnumerable<Claim> claims, int additionalMinutesFromNow)
    {
        var signingCredentials = new SigningCredentials(
            _validationParameters.IssuerSigningKey,
            SecurityAlgorithms.HmacSha256
        );

        var currentTime = DateTime.UtcNow;
        var expiredTime = currentTime.AddMinutes(additionalMinutesFromNow);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _validationParameters.ValidAudience,
            Issuer = _validationParameters.ValidIssuer,
            IssuedAt = currentTime,
            Expires = expiredTime,
            NotBefore = expiredTime - TimeSpan.FromSeconds(1),
            TokenType = AppConstant.JsonWebToken.Type.JWT,
            CompressionAlgorithm = CompressionAlgorithms.Deflate,
            SigningCredentials = signingCredentials,
            Subject = new(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtDescriptor = tokenHandler.CreateToken(tokenDescriptor);
        var jws = tokenHandler.WriteToken(jwtDescriptor);

        return jws;
    }

    /// <summary>
    ///     Is access token expired.
    /// </summary>
    /// <param name="expClaimValue">
    ///     Claim represent expiration time.
    /// </param>
    /// <returns>
    ///     True if token is expired.
    ///     False if token is not expired.
    /// </returns>
    public static bool IsAccessTokenExpired(string expClaimValue)
    {
        var tokenExpireTime = DateTimeOffset
            .FromUnixTimeSeconds(long.Parse(expClaimValue))
            .UtcDateTime;

        return tokenExpireTime <= DateTime.UtcNow;
    }
}
