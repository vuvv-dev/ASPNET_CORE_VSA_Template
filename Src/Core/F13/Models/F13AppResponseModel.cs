using F13.Common;
using FCommon.FeatureService;

namespace F13.Models;

public sealed class F13AppResponseModel : IServiceResponse
{
    public F13Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
