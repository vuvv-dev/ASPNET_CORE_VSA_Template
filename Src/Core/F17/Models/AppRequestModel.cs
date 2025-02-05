using FCommon.FeatureService;

namespace F17.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsCompleted { get; set; }
}
