using F2.Src.BusinessLogic;
using F2.Src.DataAccess;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F2.Src;

public sealed class F2Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F2Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF2Repository, F2Repository>()
            .MakeScopedLazy<IF2Repository>()
            .AddScoped<F2Service>();
        #endregion

        return services;
    }
}
