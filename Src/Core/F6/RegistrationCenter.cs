using F6.BusinessLogic;
using F6.DataAccess;
using F6.Presentation.Filters.Authorization;
using FACommon.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F6;

public sealed class RegistrationCenter : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(RegistrationCenter).Assembly;

        #region Authorization
        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                nameof(AuthorizationRequirement),
                policy =>
                    policy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .AddRequirements(new AuthorizationRequirement())
            );

        services.AddSingleton<IAuthorizationHandler, AuthorizationRequirementHandler>();
        #endregion

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IRepository, Repository>()
            .MakeScopedLazy<IRepository>()
            .AddScoped<Service>();
        #endregion

        return services;
    }
}
