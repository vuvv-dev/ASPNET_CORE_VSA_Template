using F5.Common;
using FCommon.FeatureService;

namespace F5.Models;

public sealed class F5AppResponseModel : IServiceResponse
{
    public F5Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
