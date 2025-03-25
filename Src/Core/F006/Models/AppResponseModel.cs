using F006.Common;
using FCommon.FeatureService;

namespace F006.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
