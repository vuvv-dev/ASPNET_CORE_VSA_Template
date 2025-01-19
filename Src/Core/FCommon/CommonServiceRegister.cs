using System.Security.Cryptography;
using System.Text;
using FA1.DbContext;
using FA1.Entities;
using FACommon.DependencyInjection;
using FCommon.AccessToken;
using FCommon.Authorization.Default;
using FCommon.IdGeneration;
using FCommon.RefreshToken;
using FConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FCommon;

public sealed class CommonServiceRegister : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddAppDefinedServices(services, configuration);
        AddOptions(services, configuration);
        AddDefaultAuthorization(services, configuration);

        return services;
    }

    public static void AddAppDefinedServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        #region Core
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
        #endregion
    }

    public static void AddDefaultAuthorization(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        #region Authorization
        services
            .AddAuthorizationBuilder()
            .AddDefaultPolicy(
                nameof(DefaultAuthorizationRequirement),
                policy =>
                    policy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .AddRequirements(new DefaultAuthorizationRequirement())
            );

        services.AddSingleton<IAuthorizationHandler, DefaultAuthorizationRequirementHandler>();
        #endregion
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

        var aspNetCoreIdentityOption = configuration
            .GetRequiredSection("AspNetCoreIdentity")
            .Get<AspNetCoreIdentityOptions>();

        services.AddSingleton(aspNetCoreIdentityOption);
    }
}
