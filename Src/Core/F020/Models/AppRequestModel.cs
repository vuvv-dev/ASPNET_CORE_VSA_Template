using FCommon.FeatureService;

namespace F020.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Note { get; set; }
}
