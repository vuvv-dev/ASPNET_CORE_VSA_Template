using System;

namespace F011.Models;

public sealed class TaskTodoModel
{
    public long Id { get; set; }

    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    public long TodoTaskListId { get; set; }
}
