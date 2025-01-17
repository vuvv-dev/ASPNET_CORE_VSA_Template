using FCommon.Src.FeatureService;

namespace F7.Src.Models;

public sealed class F7AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
