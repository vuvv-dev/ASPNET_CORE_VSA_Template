using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Common.DependencyInjection;

internal interface IExternalServiceRegister
{
    IServiceCollection Register(IServiceCollection services, IConfiguration configuration);
}
