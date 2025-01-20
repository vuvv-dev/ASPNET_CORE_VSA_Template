using F12.Common;
using FCommon.FeatureService;

namespace F12.Models;

public sealed class F12AppResponseModel : IServiceResponse
{
    public F12Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
