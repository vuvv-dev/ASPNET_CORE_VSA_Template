using F2.Src.BusinessLogic;
using F2.Src.DataAccess;
using FCommon.Src.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F2.Src;

public sealed class F2Register
{
    public static void RegisterF2(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IF2Repository, F2Repository>()
            .MakeScopedLazy<IF2Repository>()
            .AddScoped<F2Service>()
            .MakeScopedLazy<F2Service>();
    }
}
