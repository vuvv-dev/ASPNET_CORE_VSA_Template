using FCommon.FeatureService;

namespace F008.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
