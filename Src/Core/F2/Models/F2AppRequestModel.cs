using FCommon.FeatureService;

namespace F2.Models;

public sealed class F2AppRequestModel : IServiceRequest<F2AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
