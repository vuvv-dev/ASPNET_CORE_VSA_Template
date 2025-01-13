using FCommon.Src.FeatureService;

namespace F1.Src.Models;

public sealed class F1AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
