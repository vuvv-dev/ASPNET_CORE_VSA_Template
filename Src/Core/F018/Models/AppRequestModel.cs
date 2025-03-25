using FCommon.FeatureService;

namespace F018.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsImportant { get; set; }
}
