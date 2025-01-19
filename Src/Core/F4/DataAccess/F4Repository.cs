using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F4.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F4.DataAccess;

public sealed class F4Repository : IF4Repository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public F4Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }

    public async Task<bool> CreateResetPasswordTokenAsync(
        F4ResetPasswordTokenModel model,
        CancellationToken ct
    )
    {
        try
        {
            var resetPasswordToken = new IdentityUserTokenEntity
            {
                LoginProvider = model.LoginProvider,
                Name = model.Name,
                Value = model.Value,
                UserId = model.UserId,
                ExpiredAt = model.ExpiredAt,
            };

            await _appContext.Set<IdentityUserTokenEntity>().AddAsync(resetPasswordToken, ct);

            await _appContext.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public async Task<string> GeneratePasswordResetTokenAsync(long userId, CancellationToken ct)
    {
        var identityUser = await _userManager.Value.FindByIdAsync(userId.ToString());

        var token = await _userManager.Value.GeneratePasswordResetTokenAsync(identityUser);

        return token;
    }

    public Task<long> GetUserIdAsync(string email, CancellationToken ct)
    {
        var upperEmail = email.ToUpper();

        return _appContext
            .Set<IdentityUserEntity>()
            .AsNoTracking()
            .Where(entity => entity.NormalizedEmail.Equals(upperEmail))
            .Select(entity => entity.Id)
            .FirstOrDefaultAsync(ct);
    }
}
