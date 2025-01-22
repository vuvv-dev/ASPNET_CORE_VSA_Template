using F18.Common;
using FCommon.FeatureService;

namespace F18.Models;

public sealed class F18AppResponseModel : IServiceResponse
{
    public F18Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
