using FCommon.FeatureService;

namespace F15.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }
}
