using System.Security.Cryptography;
using System.Text;
using FA2.Src.AccessToken;
using FCommon.Src.DependencyInjection;
using FConfig.Src;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FA2.Src;

public sealed class FA2Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddJwtAuthentication(services, configuration);
        AddAppDefinedServices(services, configuration);

        return services;
    }

    private static void AddAppDefinedServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var appAuthOption = configuration
            .GetRequiredSection("Authentication")
            .GetRequiredSection("Jwt")
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
            .AddSingleton(tokenValidationParameters)
            .AddSingleton<IAppAccessTokenHandler, AppAccessTokenHandler>();
    }

    private static void AddJwtAuthentication(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var appAuthOption = configuration
            .GetRequiredSection("Authentication")
            .GetRequiredSection("Jwt")
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
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = tokenValidationParameters;
            });

        services.AddAuthorization();
    }
}
