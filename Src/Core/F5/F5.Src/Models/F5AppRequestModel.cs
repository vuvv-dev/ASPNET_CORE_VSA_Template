using FCommon.Src.FeatureService;

namespace F5.Src.Models;

public sealed class F5AppRequestModel : IServiceRequest<F5AppResponseModel>
{
    public string ResetPasswordToken { get; set; }
}
