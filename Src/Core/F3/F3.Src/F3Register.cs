using F3.Src.BusinessLogic;
using F3.Src.DataAccess;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F3.Src;

public sealed class F3Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F3Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF3Repository, F3Repository>()
            .MakeScopedLazy<IF3Repository>()
            .AddScoped<F3Service>();
        #endregion

        return services;
    }
}
