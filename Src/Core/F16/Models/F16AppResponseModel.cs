using F16.Common;
using FCommon.FeatureService;

namespace F16.Models;

public sealed class F16AppResponseModel : IServiceResponse
{
    public F16Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
