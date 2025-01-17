using F6.Src.Common;
using FCommon.Src.FeatureService;

namespace F6.Src.Models;

public sealed class F6AppResponseModel : IServiceResponse
{
    public F6Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
