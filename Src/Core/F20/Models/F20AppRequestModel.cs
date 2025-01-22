using FCommon.FeatureService;

namespace F20.Models;

public sealed class F20AppRequestModel : IServiceRequest<F20AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public string Note { get; set; }
}
