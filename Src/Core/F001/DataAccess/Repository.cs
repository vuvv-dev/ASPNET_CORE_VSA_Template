using System;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F001.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F001.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _context;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;
    private readonly Lazy<SignInManager<IdentityUserEntity>> _signInManager;

    public Repository(
        AppDbContext context,
        Lazy<UserManager<IdentityUserEntity>> userManager,
        Lazy<SignInManager<IdentityUserEntity>> signInManager
    )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<PasswordSignInResultModel> CheckPasswordSignInAsync(
        string email,
        string password,
        CancellationToken ct
    )
    {
        var foundUser = await _userManager.Value.FindByEmailAsync(email);
        var passwordValidatingResult = await _signInManager.Value.CheckPasswordSignInAsync(
            foundUser,
            password,
            lockoutOnFailure: true
        );

        return new()
        {
            IsSuccessful = passwordValidatingResult.Succeeded,
            IsLockedOut = passwordValidatingResult.IsLockedOut,
            UserId = foundUser.Id,
        };
    }

    public async Task<bool> CreateRefreshTokenAsync(RefreshTokenModel model, CancellationToken ct)
    {
        try
        {
            await _context
                .Set<IdentityUserTokenEntity>()
                .AddAsync(
                    new()
                    {
                        LoginProvider = model.LoginProvider,
                        ExpiredAt = model.ExpiredAt,
                        UserId = model.UserId,
                        Value = model.Value,
                        Name = model.Name,
                    },
                    ct
                );

            await _context.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public Task<bool> IsUserFoundByEmailAsync(string email, CancellationToken ct)
    {
        var upperCaseEmail = email.ToUpper();

        return _context
            .Set<IdentityUserEntity>()
            .AnyAsync(user => user.NormalizedEmail.Equals(upperCaseEmail), ct);
    }
}
