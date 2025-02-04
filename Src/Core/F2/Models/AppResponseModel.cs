using F2.Common;
using FCommon.FeatureService;

namespace F2.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public TodoTaskListModel TodoTaskList { get; set; }

        public sealed class TodoTaskListModel
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
