using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace F014.Presentation;

public sealed class Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public IEnumerable<TodoTaskDto> TodoTasks { get; set; }

        public long NextCursor { get; set; }

        public sealed class TodoTaskDto
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
