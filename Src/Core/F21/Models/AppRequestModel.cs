using System;
using FCommon.FeatureService;

namespace F21.Models;

public sealed class AppRequestModel : IServiceRequest<AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public DateTime DueDate { get; set; }
}
