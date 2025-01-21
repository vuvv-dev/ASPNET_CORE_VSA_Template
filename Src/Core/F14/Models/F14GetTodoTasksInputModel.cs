namespace F14.Models;

public sealed class F14GetTodoTasksInputModel
{
    public long TodoTaskId { get; set; }

    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
