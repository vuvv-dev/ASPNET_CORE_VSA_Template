using FCommon.FeatureService;

namespace F10.Models;

public sealed class F10AppRequestModel : IServiceRequest<F10AppResponseModel>
{
    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
