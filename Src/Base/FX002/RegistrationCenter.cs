using System.Security.Cryptography;
using System.Text;
using Base.Common.DependencyInjection;
using Base.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Base.FX002;

internal sealed class RegistrationCenter : IExternalServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddJwtAuthentication(services, configuration);

        return services;
    }

    private static void AddJwtAuthentication(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var appAuthOption = configuration
            .GetRequiredSection("Authentication")
            .GetRequiredSection("Jwt")
            .GetRequiredSection("User")
            .Get<JwtAuthenticationOption>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = appAuthOption.ValidateIssuer,
            ValidateAudience = appAuthOption.ValidateAudience,
            ValidateLifetime = appAuthOption.ValidateLifetime,
            ValidateIssuerSigningKey = appAuthOption.ValidateIssuerSigningKey,
            RequireExpirationTime = appAuthOption.RequireExpirationTime,
            ValidTypes = appAuthOption.ValidTypes,
            ValidIssuer = appAuthOption.ValidIssuer,
            ValidAudience = appAuthOption.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                new HMACSHA256(Encoding.UTF8.GetBytes(appAuthOption.IssuerSigningKey)).Key
            ),
        };

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                config =>
                {
                    config.TokenValidationParameters = tokenValidationParameters;
                }
            );
    }
}
