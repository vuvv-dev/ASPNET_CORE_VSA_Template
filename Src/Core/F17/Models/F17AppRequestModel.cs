using FCommon.FeatureService;

namespace F17.Models;

public sealed class F17AppRequestModel : IServiceRequest<F17AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsCompleted { get; set; }
}
