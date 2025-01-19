using F5.BusinessLogic;
using F5.DataAccess;
using F5.Presentation.Filters.Authorization;
using FACommon.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F5;

public sealed class F5Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F5Register).Assembly;

        #region Authorization
        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                nameof(F5AuthorizationRequirement),
                policy =>
                    policy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .AddRequirements(new F5AuthorizationRequirement())
            );

        services.AddSingleton<IAuthorizationHandler, F5AuthorizationRequirementHandler>();
        #endregion

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF5Repository, F5Repository>()
            .MakeScopedLazy<IF5Repository>()
            .AddScoped<F5Service>();
        #endregion

        return services;
    }
}
