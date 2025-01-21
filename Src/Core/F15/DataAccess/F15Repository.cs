using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F15.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F15.DataAccess;

public sealed class F15Repository : IF15Repository
{
    private readonly AppDbContext _appContext;

    public F15Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskEntity>().AnyAsync(entity => entity.Id == taskId, ct);
    }

    public async Task<F15TodoTaskModel> GetTaskDetailByIdAsync(long taskId, CancellationToken ct)
    {
        var foundTask = await _appContext
            .Set<TodoTaskEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == taskId)
            .Select(entity => new TodoTaskEntity
            {
                Content = entity.Content,
                DueDate = entity.DueDate,
                IsInMyDay = entity.IsInMyDay,
                IsImportant = entity.IsImportant,
                Note = entity.Note,
                IsFinished = entity.IsFinished,
            })
            .FirstOrDefaultAsync(ct);

        return new()
        {
            Content = foundTask.Content,
            DueDate = foundTask.DueDate,
            IsInMyDay = foundTask.IsInMyDay,
            IsImportant = foundTask.IsImportant,
            Note = foundTask.Note,
            IsFinished = foundTask.IsFinished,
        };
    }
}
