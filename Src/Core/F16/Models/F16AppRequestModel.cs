using FCommon.FeatureService;

namespace F16.Models;

public sealed class F16AppRequestModel : IServiceRequest<F16AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsInMyDay { get; set; }
}
