using FCommon.FeatureService;

namespace F8.Models;

public sealed class F8AppRequestModel : IServiceRequest<F8AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
