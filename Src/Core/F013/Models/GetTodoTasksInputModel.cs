namespace F013.Models;

public sealed class GetTodoTasksInputModel
{
    public long TodoTaskId { get; set; }

    public long TodoTaskListId { get; set; }

    public int NumberOfRecord { get; set; }
}
