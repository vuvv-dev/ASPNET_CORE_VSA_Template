using System.Threading;
using System.Threading.Tasks;

namespace FCommon.FeatureService;

public interface IServiceHandler<TRequest, TResponse>
    where TRequest : IServiceRequest<TResponse>
    where TResponse : IServiceResponse
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken ct);
}
