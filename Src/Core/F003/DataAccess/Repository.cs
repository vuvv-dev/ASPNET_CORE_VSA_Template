using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F003.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F003.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(UserInfoModel user, CancellationToken ct)
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
                    var newIdentityUser = new IdentityUserEntity
                    {
                        Id = user.Id,
                        UserName = user.Email,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                    };
                    var result = await _userManager.Value.CreateAsync(
                        newIdentityUser,
                        user.Password
                    );
                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException();
                    }

                    var additionalUserInfo = new AdditionalUserInfoEntity
                    {
                        Id = user.Id,
                        FirstName = user.AdditionalUserInfo.FirstName,
                        LastName = user.AdditionalUserInfo.LastName,
                        Description = user.AdditionalUserInfo.Description,
                    };

                    await _appContext
                        .Set<AdditionalUserInfoEntity>()
                        .AddAsync(additionalUserInfo, ct);
                    await _appContext.SaveChangesAsync(ct);

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

    public Task<bool> DoesEmailExistsAsync(string email, CancellationToken ct)
    {
        var upperEmail = email.ToUpper();

        return _appContext
            .Set<IdentityUserEntity>()
            .AnyAsync(user => user.NormalizedEmail.Equals(upperEmail), ct);
    }

    public async Task<bool> IsPasswordValidAsync(
        string email,
        string password,
        CancellationToken ct
    )
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
}
