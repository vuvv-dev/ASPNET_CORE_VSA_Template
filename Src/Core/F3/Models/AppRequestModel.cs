using FCommon.FeatureService;

namespace F3.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
