using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FACommon.DependencyInjection;

public interface IServiceRegister
{
    IServiceCollection Register(IServiceCollection services, IConfiguration configuration);
}
