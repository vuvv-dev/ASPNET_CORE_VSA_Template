using F19.Common;
using FCommon.FeatureService;

namespace F19.Models;

public sealed class F19AppResponseModel : IServiceResponse
{
    public F19Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
