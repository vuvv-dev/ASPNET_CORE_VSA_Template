using System;

namespace F007.Models;

public class TaskTodoListModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public long UserId { get; set; }
}
