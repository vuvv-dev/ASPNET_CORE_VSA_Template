using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FCommon.Src.Constants;
using Microsoft.IdentityModel.Tokens;

namespace FCommon.Src.AccessToken;

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
            TokenType = AppConstants.JsonWebToken.Type.JWS,
            CompressionAlgorithm = CompressionAlgorithms.Deflate,
            SigningCredentials = signingCredentials,
            Subject = new(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtDescriptor = tokenHandler.CreateToken(tokenDescriptor);
        var jws = tokenHandler.WriteToken(jwtDescriptor);

        return jws;
    }
}
