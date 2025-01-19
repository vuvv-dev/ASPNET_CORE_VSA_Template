using FCommon.FeatureService;

namespace F4.Models;

public sealed class F4AppRequestModel : IServiceRequest<F4AppResponseModel>
{
    public string Email { get; set; }
}
