using FCommon.FeatureService;

namespace F12.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }
}
