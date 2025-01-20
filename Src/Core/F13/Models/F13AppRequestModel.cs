using FCommon.FeatureService;

namespace F13.Models;

public sealed class F13AppRequestModel : IServiceRequest<F13AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
