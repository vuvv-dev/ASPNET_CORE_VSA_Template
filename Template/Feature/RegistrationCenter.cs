using FACommon.DependencyInjection;
using FeatTemplate.BusinessLogic;
using FeatTemplate.DataAccess;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatTemplate;

internal sealed class RegistrationCenter : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(RegistrationCenter).Assembly;

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
