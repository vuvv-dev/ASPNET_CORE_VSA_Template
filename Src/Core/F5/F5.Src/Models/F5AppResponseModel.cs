using F5.Src.Common;
using FCommon.Src.FeatureService;

namespace F5.Src.Models;

public sealed class F5AppResponseModel : IServiceResponse
{
    public F5Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
