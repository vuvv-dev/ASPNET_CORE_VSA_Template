using FCommon.FeatureService;

namespace F11.Models;

public sealed class F11AppRequestModel : IServiceRequest<F11AppResponseModel>
{
    public string Content { get; set; }

    public long TodoTaskListId { get; set; }
}
