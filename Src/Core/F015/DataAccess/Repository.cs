using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F015.Models;
using Microsoft.EntityFrameworkCore;

namespace F015.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskEntity>().AnyAsync(entity => entity.Id == taskId, ct);
    }

    public async Task<TodoTaskModel> GetTaskDetailByIdAsync(long taskId, CancellationToken ct)
    {
        return await _appContext
            .Set<TodoTaskEntity>()
            .Where(entity => entity.Id == taskId)
            .Select(entity => new TodoTaskModel
            {
                Content = entity.Content,
                DueDate = entity.DueDate,
                IsExpired = DateTime.UtcNow > entity.DueDate && entity.DueDate != DateTime.MinValue,
                IsInMyDay = entity.IsInMyDay,
                IsImportant = entity.IsImportant,
                Note = entity.Note,
                IsCompleted = entity.IsFinished,
            })
            .FirstOrDefaultAsync(ct);
    }
}
