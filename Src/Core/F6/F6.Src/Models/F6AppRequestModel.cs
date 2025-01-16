using FCommon.Src.FeatureService;

namespace F6.Src.Models;

public sealed class F6AppRequestModel : IServiceRequest<F6AppResponseModel>
{
    public long AccessTokenId { get; set; }

    public string RefreshToken { get; set; }
}
