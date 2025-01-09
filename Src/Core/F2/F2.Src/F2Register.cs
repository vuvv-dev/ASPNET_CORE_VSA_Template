using F2.Src.BusinessLogic;
using F2.Src.DataAccess;
using FCommon.Src.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F2.Src;

public sealed class F2Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        #region Filters
        services = services.RegisterFiltersFromAssembly(typeof(F2Register));
        #endregion

        #region Core
        services
            .AddScoped<IF2Repository, F2Repository>()
            .MakeScopedLazy<IF2Repository>()
            .AddScoped<F2Service>()
            .MakeScopedLazy<F2Service>();
        #endregion

        return services;
    }
}
