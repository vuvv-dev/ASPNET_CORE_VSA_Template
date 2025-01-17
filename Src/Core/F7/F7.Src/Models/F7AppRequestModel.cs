using FCommon.Src.FeatureService;

namespace F7.Src.Models;

public sealed class F7AppRequestModel : IServiceRequest<F7AppResponseModel>
{
    public string TodoTaskListName { get; set; }

    public long UserId { get; set; }
}
