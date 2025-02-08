using RFeatureCommonModule.FeatureService;

namespace FeatTemplate.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
