using FCommon.FeatureService;

namespace F19.Models;

public sealed class F19AppRequestModel : IServiceRequest<F19AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Content { get; set; }
}
