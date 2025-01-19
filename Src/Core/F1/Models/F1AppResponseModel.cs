using F1.Common;
using FCommon.FeatureService;

namespace F1.Models;

public sealed class F1AppResponseModel : IServiceResponse
{
    public F1Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
