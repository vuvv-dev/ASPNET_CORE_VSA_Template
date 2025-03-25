using F004.Common;
using FCommon.FeatureService;

namespace F004.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string ResetPasswordToken { get; set; }
    }
}
