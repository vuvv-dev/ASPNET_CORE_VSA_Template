using FCommon.FeatureService;

namespace F19.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Content { get; set; }
}
