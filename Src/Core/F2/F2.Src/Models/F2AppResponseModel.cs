using System;
using FCommon.Src.FeatureService;

namespace F2.Src.Models;

public sealed class F2AppResponseModel : IServiceResponse
{
    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
