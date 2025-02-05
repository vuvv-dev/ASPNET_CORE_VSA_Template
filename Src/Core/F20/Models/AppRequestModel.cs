using FCommon.FeatureService;

namespace F20.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Note { get; set; }
}
