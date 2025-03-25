using System;
using System.Text.Json.Serialization;

namespace F015.Presentation;

public sealed class Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public TodoTaskDto TodoTask { get; set; }

        public sealed class TodoTaskDto
        {
            public long Id { get; set; }

            public string Content { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsExpired { get; set; }

            public bool IsInMyDay { get; set; }

            public bool IsImportant { get; set; }

            public string Note { get; set; }

            public bool IsCompleted { get; set; }
        }
    }
}
