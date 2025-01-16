using F6.Src.BusinessLogic;
using F6.Src.DataAccess;
using F6.Src.Presentation.Filters.Authorization;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F6.Src;

public sealed class F6Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F6Register).Assembly;

        #region Authorization
        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                nameof(F6AuthorizationRequirement),
                policy =>
                    policy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .AddRequirements(new F6AuthorizationRequirement())
            );

        services.AddSingleton<IAuthorizationHandler, F6AuthorizationRequirementHandler>();
        #endregion

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF6Repository, F6Repository>()
            .MakeScopedLazy<IF6Repository>()
            .AddScoped<F6Service>();
        #endregion

        return services;
    }
}
