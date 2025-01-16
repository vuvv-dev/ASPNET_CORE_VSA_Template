using FCommon.Src.FeatureService;

namespace F6.Src.Models;

public sealed class F6AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
