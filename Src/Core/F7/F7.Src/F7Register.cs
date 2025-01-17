using F7.Src.BusinessLogic;
using F7.Src.DataAccess;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F7.Src;

public sealed class F7Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F7Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF7Repository, F7Repository>()
            .MakeScopedLazy<IF7Repository>()
            .AddScoped<F7Service>();
        #endregion

        return services;
    }
}
