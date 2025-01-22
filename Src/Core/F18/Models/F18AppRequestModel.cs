using FCommon.FeatureService;

namespace F18.Models;

public sealed class F18AppRequestModel : IServiceRequest<F18AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public bool IsImportant { get; set; }
}
