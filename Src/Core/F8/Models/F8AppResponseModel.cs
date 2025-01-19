using F8.Common;
using FCommon.FeatureService;

namespace F8.Models;

public sealed class F8AppResponseModel : IServiceResponse
{
    public F8Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
