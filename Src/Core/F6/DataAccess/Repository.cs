using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F6.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F6.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<RefreshTokenModel> DoesRefreshTokenBelongToAccessTokenAsync(
        string refreshTokenId,
        string refreshTokenValue,
        CancellationToken ct
    )
    {
        var foundToken = await _appContext
            .Set<IdentityUserTokenEntity>()
            .AsNoTracking()
            .Where(token =>
                token.LoginProvider.Equals(refreshTokenId) && token.Value.Equals(refreshTokenValue)
            )
            .Select(token => new IdentityUserTokenEntity { ExpiredAt = token.ExpiredAt })
            .FirstOrDefaultAsync(ct);

        if (Equals(foundToken, null))
        {
            return null;
        }

        var refreshTokenModel = new RefreshTokenModel { ExpiredAt = foundToken.ExpiredAt };

        return refreshTokenModel;
    }

    public async Task<bool> UpdateRefreshTokenAsync(
        UpdateRefreshTokenModel model,
        CancellationToken ct
    )
    {
        var dbResult = true;

        await _appContext
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var dbTransaction = await _appContext.Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    ct
                );

                try
                {
                    var rowsAffected = await _appContext
                        .Set<IdentityUserTokenEntity>()
                        .Where(token => token.LoginProvider.Equals(model.CurrentId))
                        .ExecuteUpdateAsync(
                            setProp =>
                                setProp.SetProperty(entity => entity.LoginProvider, model.NewId),
                            ct
                        );

                    if (rowsAffected == 0)
                    {
                        throw new DbUpdateException();
                    }

                    await dbTransaction.CommitAsync(ct);
                }
                catch (DbUpdateException)
                {
                    await dbTransaction.RollbackAsync(ct);

                    dbResult = false;
                }
            });

        return dbResult;
    }
}
