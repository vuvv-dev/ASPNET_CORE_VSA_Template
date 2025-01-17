using FCommon.Src.FeatureService;

namespace F8.Src.Models;

public sealed class F8AppRequestModel : IServiceRequest<F8AppResponseModel>
{
    public long TodoTaskListId { get; set; }
}
