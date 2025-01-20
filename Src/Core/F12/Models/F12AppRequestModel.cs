using FCommon.FeatureService;

namespace F12.Models;

public sealed class F12AppRequestModel : IServiceRequest<F12AppResponseModel>
{
    public long TodoTaskId { get; set; }
}
