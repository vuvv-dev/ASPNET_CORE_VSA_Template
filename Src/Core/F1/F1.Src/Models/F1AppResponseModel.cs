using F1.Src.Common;
using FCommon.Src.FeatureService;

namespace F1.Src.Models;

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
