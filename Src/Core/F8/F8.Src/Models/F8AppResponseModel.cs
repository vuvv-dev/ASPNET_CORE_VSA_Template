using FCommon.Src.FeatureService;

namespace F8.Src.Models;

public sealed class F8AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
