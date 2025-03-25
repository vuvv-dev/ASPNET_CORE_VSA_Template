using FCommon.FeatureService;

namespace F002.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
