using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F5.Common;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F5.DataAccess;

public sealed class F5Repository : IF5Repository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public F5Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }

    public Task<string> GetResetPasswordTokenByIdAsync(
        string resetPasswordTokenId,
        CancellationToken ct
    )
    {
        return _appContext
            .Set<IdentityUserTokenEntity>()
            .Where(token => token.LoginProvider.Equals(resetPasswordTokenId))
            .Select(token => token.Value)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> IsPasswordValidAsync(string password, CancellationToken ct)
    {
        IdentityResult result = default;

        foreach (var validator in _userManager.Value.PasswordValidators)
        {
            result = await validator.ValidateAsync(
                _userManager.Value,
                new() { Id = default },
                password
            );
        }

        if (Equals(result, default))
        {
            return false;
        }

        return result.Succeeded;
    }

    public async Task<bool> ResetPasswordAsync(
        long userId,
        string password,
        string passwordResetToken,
        string passwordResetTokenId,
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
                    var user = await _userManager.Value.FindByIdAsync(userId.ToString());
                    var resetPasswordResult = await _userManager.Value.ResetPasswordAsync(
                        user,
                        passwordResetToken,
                        password
                    );

                    if (!resetPasswordResult.Succeeded)
                    {
                        throw new DbUpdateException();
                    }

                    // Remove reset password token
                    var rowsAffected = await _appContext
                        .Set<IdentityUserTokenEntity>()
                        .Where(token => token.LoginProvider.Equals(passwordResetTokenId))
                        .ExecuteDeleteAsync(ct);

                    if (rowsAffected == 0)
                    {
                        throw new DbUpdateException();
                    }

                    // Remove all user refresh tokens which
                    // haves the same id as access token id
                    await _appContext
                        .Set<IdentityUserTokenEntity>()
                        .Where(token =>
                            token.UserId == userId
                            && token.Name.Equals(F5Constant.APP_USER_REFRESH_TOKEN.NAME)
                        )
                        .ExecuteDeleteAsync(ct);

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
