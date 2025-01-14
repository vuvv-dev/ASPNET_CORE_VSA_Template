using FCommon.Src.FeatureService;

namespace F3.Src.Models;

public sealed class F3AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
