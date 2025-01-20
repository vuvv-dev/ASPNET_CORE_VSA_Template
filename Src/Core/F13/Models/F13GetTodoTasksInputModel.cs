namespace F13.Models;

public sealed class F13GetTodoTasksInputModel
{
    public long TodoTaskId { get; set; }

    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
