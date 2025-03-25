using FCommon.FeatureService;

namespace F016.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsInMyDay { get; set; }
}
