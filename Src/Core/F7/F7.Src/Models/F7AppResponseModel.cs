using F7.Src.Common;
using FCommon.Src.FeatureService;

namespace F7.Src.Models;

public sealed class F7AppResponseModel : IServiceResponse
{
    public F7Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public long ListId { get; set; }
    }
}
