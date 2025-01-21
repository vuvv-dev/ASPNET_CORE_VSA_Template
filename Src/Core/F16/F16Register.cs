using F16.BusinessLogic;
using F16.DataAccess;
using FACommon.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F16;

public sealed class F16Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F16Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF16Repository, F16Repository>()
            .MakeScopedLazy<IF16Repository>()
            .AddScoped<F16Service>();
        #endregion

        return services;
    }
}
