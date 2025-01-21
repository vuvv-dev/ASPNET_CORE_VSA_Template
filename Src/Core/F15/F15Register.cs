using F15.BusinessLogic;
using F15.DataAccess;
using FACommon.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F15;

public sealed class F15Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F15Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF15Repository, F15Repository>()
            .MakeScopedLazy<IF15Repository>()
            .AddScoped<F15Service>();
        #endregion

        return services;
    }
}
