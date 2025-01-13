using FCommon.Src.FeatureService;

namespace F1.Src.Models;

public sealed class F1AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
