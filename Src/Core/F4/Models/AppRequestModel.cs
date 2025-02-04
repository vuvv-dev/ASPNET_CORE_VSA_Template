using FCommon.FeatureService;

namespace F4.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Email { get; set; }
}
