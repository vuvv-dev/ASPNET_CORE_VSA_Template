using F9.Src.Common;
using FCommon.Src.FeatureService;

namespace F9.Src.Models;

public sealed class F9AppResponseModel : IServiceResponse
{
    public F9Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
