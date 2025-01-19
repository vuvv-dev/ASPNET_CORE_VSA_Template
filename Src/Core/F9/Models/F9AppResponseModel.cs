using F9.Common;
using FCommon.FeatureService;

namespace F9.Models;

public sealed class F9AppResponseModel : IServiceResponse
{
    public F9Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
