using F007.Common;
using FCommon.FeatureService;

namespace F007.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public long ListId { get; set; }
    }
}
