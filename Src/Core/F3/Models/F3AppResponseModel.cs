using F3.Common;
using FCommon.FeatureService;

namespace F3.Models;

public sealed class F3AppResponseModel : IServiceResponse
{
    public F3Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
