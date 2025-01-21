using FCommon.FeatureService;

namespace F15.Models;

public sealed class F15AppRequestModel : IServiceRequest<F15AppResponseModel>
{
    public long TodoTaskId { get; set; }
}
