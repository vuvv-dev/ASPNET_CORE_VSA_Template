using FCommon.FeatureService;

namespace F14.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
