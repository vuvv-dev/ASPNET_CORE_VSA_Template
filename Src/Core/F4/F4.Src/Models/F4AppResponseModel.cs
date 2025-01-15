using FCommon.Src.FeatureService;

namespace F4.Src.Models;

public sealed class F4AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string Token { get; set; }
    }
}
