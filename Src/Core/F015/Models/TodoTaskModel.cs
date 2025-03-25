using System;

namespace F015.Models;

public sealed class TodoTaskModel
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
