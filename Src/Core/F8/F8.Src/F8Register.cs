using F8.Src.BusinessLogic;
using F8.Src.DataAccess;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F8.Src;

public sealed class F8Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F8Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF8Repository, F8Repository>()
            .MakeScopedLazy<IF8Repository>()
            .AddScoped<F8Service>();
        #endregion

        return services;
    }
}
