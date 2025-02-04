using FCommon.FeatureService;

namespace F2.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
