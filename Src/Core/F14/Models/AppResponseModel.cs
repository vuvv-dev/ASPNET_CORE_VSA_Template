using System;
using System.Collections.Generic;
using F14.Common;
using FCommon.FeatureService;

namespace F14.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public IEnumerable<TodoTaskModel> TodoTasks { get; set; }

        public long NextCursor { get; set; }

        public sealed class TodoTaskModel
        {
            public long Id { get; set; }

            public string Content { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsImportant { get; set; }

            public bool IsInMyDay { get; set; }

            public bool HasNote { get; set; }

            public bool HasSteps { get; set; }

            public bool IsRecurring { get; set; }
        }
    }
}
