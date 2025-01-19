using System.Collections.Generic;
using F10.Src.Common;
using FCommon.Src.FeatureService;

namespace F10.Src.Models;

public sealed class F10AppResponseModel : IServiceResponse
{
    public F10Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public IEnumerable<TodoTaskListModel> TodoTaskLists { get; set; }

        public long NextCursor { get; set; }

        public sealed class TodoTaskListModel
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
