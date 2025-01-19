using FCommon.FeatureService;

namespace F7.Models;

public sealed class F7AppRequestModel : IServiceRequest<F7AppResponseModel>
{
    public string TodoTaskListName { get; set; }

    public long UserId { get; set; }
}
