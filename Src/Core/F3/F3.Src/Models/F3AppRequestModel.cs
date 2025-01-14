using FCommon.Src.FeatureService;

namespace F3.Src.Models;

public sealed class F3AppRequestModel : IServiceRequest<F3AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
