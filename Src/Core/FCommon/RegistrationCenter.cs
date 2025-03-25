using Base.Common.DependencyInjection;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using FCommon.AccessToken;
using FCommon.Authorization.Default;
using FCommon.IdGeneration;
using FCommon.RefreshToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCommon;

internal sealed class RegistrationCenter : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddAppDefinedServices(services, configuration);
        AddDefaultAuthorization(services, configuration);

        return services;
    }

    private static void AddAppDefinedServices(
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

    internal static void AddDefaultAuthorization(
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
}
