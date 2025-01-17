using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace F2.Src.Presentation;

public sealed class F2Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public TodoTaskListDto TodoTaskList { get; set; }

        public sealed class TodoTaskListDto
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public IEnumerable<TodoTaskDto> TodoTasks { get; set; }

            public sealed class TodoTaskDto
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
}
