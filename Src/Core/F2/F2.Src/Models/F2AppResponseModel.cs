using System;
using System.Collections.Generic;
using F2.Src.Common;
using FCommon.Src.FeatureService;

namespace F2.Src.Models;

public sealed class F2AppResponseModel : IServiceResponse
{
    public F2Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TodoTaskModel> TodoTasks { get; set; }

        public sealed class TodoTaskModel
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsInMyDay { get; set; }

            public bool IsImportant { get; set; }

            public bool IsFinished { get; set; }
        }
    }
}
