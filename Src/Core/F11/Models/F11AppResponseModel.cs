using F11.Common;
using FCommon.FeatureService;

namespace F11.Models;

public sealed class F11AppResponseModel : IServiceResponse
{
    public F11Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public long TodoTaskId { get; set; }
    }
}
