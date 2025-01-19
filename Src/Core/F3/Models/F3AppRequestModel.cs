using FCommon.FeatureService;

namespace F3.Models;

public sealed class F3AppRequestModel : IServiceRequest<F3AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
