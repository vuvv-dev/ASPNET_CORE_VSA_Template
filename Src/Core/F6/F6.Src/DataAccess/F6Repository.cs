using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F6.Src.Models;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F6.Src.DataAccess;

public sealed class F6Repository : IF6Repository
{
    private readonly AppDbContext _appContext;

    public F6Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<F6RefreshTokenModel> DoesRefreshTokenBelongToAccessTokenAsync(
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

        var refreshTokenModel = new F6RefreshTokenModel { ExpiredAt = foundToken.ExpiredAt };

        return refreshTokenModel;
    }

    public async Task<bool> UpdateRefreshTokenAsync(
        F6UpdateRefreshTokenModel model,
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
                                setProp
                                    .SetProperty(entity => entity.Value, model.NewValue)
                                    .SetProperty(entity => entity.LoginProvider, model.NewId),
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
