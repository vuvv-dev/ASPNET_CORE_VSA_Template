using FCommon.FeatureService;

namespace F004.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Email { get; set; }
}
