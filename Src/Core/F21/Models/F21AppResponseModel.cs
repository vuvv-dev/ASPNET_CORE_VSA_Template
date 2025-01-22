using F21.Common;
using FCommon.FeatureService;

namespace F21.Models;

public sealed class F21AppResponseModel : IServiceResponse
{
    public F21Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
