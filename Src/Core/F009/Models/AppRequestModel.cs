using FCommon.FeatureService;

namespace F009.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskListId { get; set; }

    public string NewName { get; set; }
}
