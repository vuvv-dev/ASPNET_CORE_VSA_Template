using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FA2.Src.Common;
using Microsoft.IdentityModel.Tokens;

namespace FA2.Src.AccessToken;

public sealed class AppAccessTokenHandler : IAppAccessTokenHandler
{
    private readonly TokenValidationParameters _validationParameters;

    public AppAccessTokenHandler(TokenValidationParameters validationParameters)
    {
        _validationParameters = validationParameters;
    }

    public string GenerateJWS(IEnumerable<Claim> claims)
    {
        var isExpiredTimeValid = DateTime.TryParse(
            claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Expired).Value,
            out var expiredTime
        );
        if (!isExpiredTimeValid)
        {
            return string.Empty;
        }

        var signingCredentials = new SigningCredentials(
            _validationParameters.IssuerSigningKey,
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _validationParameters.ValidAudience,
            Issuer = _validationParameters.ValidIssuer,
            IssuedAt = DateTime.UtcNow,
            Expires = expiredTime,
            TokenType = FA2Constant.JsonWebToken.JWS,
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
