using F4.Common;
using FCommon.FeatureService;

namespace F4.Models;

public sealed class F4AppResponseModel : IServiceResponse
{
    public F4Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string ResetPasswordToken { get; set; }
    }
}
