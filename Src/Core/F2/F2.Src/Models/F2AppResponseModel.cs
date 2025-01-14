using System;
using System.Collections.Generic;
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

        public IEnumerable<TodoTaskModel> TodoTasks { get; set; }

        public sealed class TodoTaskModel
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public DateTime DueDate { get; set; }
        }
    }
}
