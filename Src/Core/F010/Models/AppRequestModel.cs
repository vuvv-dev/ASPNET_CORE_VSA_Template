using FCommon.FeatureService;

namespace F010.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
