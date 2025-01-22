using System;
using FCommon.FeatureService;

namespace F21.Models;

public sealed class F21AppRequestModel : IServiceRequest<F21AppResponseModel>
{
    public long TodoTaskId { get; set; }

    public DateTime DueDate { get; set; }
}
