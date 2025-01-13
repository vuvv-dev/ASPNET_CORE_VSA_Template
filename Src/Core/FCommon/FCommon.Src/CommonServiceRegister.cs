using System.Security.Cryptography;
using System.Text;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using FACommon.Src.DependencyInjection;
using FCommon.Src.AccessToken;
using FCommon.Src.IdGeneration;
using FCommon.Src.RefreshToken;
using FConfig.Src;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FCommon.Src;

public sealed class CommonServiceRegister : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddAppDefinedServices(services, configuration);
        AddOptions(services, configuration);

        return services;
    }

    public static void AddAppDefinedServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IAppIdGenerator, SnowflakeIdGenerator>()
            .MakeSingletonLazy<IAppIdGenerator>()
            .AddSingleton<IAppAccessTokenHandler, AppAccessTokenHandler>()
            .MakeSingletonLazy<IAppAccessTokenHandler>()
            .AddSingleton<IAppRefreshTokenHandler, AppRefreshTokenHandler>()
            .MakeSingletonLazy<IAppRefreshTokenHandler>()
            .MakeScopedLazy<AppDbContext>()
            .MakeScopedLazy<UserManager<IdentityUserEntity>>()
            .MakeScopedLazy<RoleManager<IdentityRoleEntity>>()
            .MakeScopedLazy<SignInManager<IdentityUserEntity>>();
    }

    public static void AddOptions(IServiceCollection services, IConfiguration configuration)
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

        services.AddSingleton(tokenValidationParameters);
    }
}
