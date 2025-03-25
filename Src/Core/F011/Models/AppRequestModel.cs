using FCommon.FeatureService;

namespace F011.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Content { get; set; }

    public long TodoTaskListId { get; set; }
}
