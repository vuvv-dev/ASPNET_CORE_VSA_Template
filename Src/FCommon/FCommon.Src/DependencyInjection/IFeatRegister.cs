using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCommon.Src.DependencyInjection;

public interface IFeatRegister
{
    void Register(IServiceCollection services, IConfiguration configuration);
}
