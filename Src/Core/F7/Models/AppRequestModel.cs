using FCommon.FeatureService;

namespace F7.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string TodoTaskListName { get; set; }

    public long UserId { get; set; }
}
