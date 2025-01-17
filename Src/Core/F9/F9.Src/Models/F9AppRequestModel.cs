using FCommon.Src.FeatureService;

namespace F9.Src.Models;

public sealed class F9AppRequestModel : IServiceRequest<F9AppResponseModel>
{
    public long TodoTaskListId { get; set; }

    public string NewName { get; set; }
}
