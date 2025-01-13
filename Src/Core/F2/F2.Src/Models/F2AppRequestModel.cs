using FCommon.Src.FeatureService;

namespace F2.Src.Models;

public sealed class F2AppRequestModel : IServiceRequest<F2AppResponseModel>
{
    public long ListId { get; set; }
}
