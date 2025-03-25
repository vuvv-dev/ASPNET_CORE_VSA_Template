using FCommon.FeatureService;

namespace F019.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Content { get; set; }
}
