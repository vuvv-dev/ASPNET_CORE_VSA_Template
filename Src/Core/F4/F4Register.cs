using F4.BusinessLogic;
using F4.DataAccess;
using FACommon.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F4;

public sealed class F4Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F4Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF4Repository, F4Repository>()
            .MakeScopedLazy<IF4Repository>()
            .AddScoped<F4Service>();
        #endregion

        return services;
    }
}
