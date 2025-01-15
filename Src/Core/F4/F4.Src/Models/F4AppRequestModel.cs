using FCommon.Src.FeatureService;

namespace F4.Src.Models;

public sealed class F4AppRequestModel : IServiceRequest<F4AppResponseModel>
{
    public string Email { get; set; }
}
