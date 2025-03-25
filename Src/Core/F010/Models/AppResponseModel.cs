using System.Collections.Generic;
using F010.Common;
using FCommon.FeatureService;

namespace F010.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

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
