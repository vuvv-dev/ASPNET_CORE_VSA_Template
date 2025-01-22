using F20.Common;
using FCommon.FeatureService;

namespace F20.Models;

public sealed class F20AppResponseModel : IServiceResponse
{
    public F20Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
