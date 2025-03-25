using FCommon.FeatureService;

namespace F005.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long ResetPasswordTokenId { get; set; }

    public long UserId { get; set; }

    public string NewPassword { get; set; }
}
