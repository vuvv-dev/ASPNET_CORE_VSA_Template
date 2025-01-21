using F17.Common;
using FCommon.FeatureService;

namespace F17.Models;

public sealed class F17AppResponseModel : IServiceResponse
{
    public F17Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
