using System;
using System.Threading;
using System.Threading.Tasks;
using F1.Src.Models;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F1.Src.DataAccess;

public sealed class F1Repository : IF1Repository
{
    private readonly AppDbContext _context;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;
    private readonly Lazy<SignInManager<IdentityUserEntity>> _signInManager;

    public F1Repository(
        AppDbContext context,
        Lazy<UserManager<IdentityUserEntity>> userManager,
        Lazy<SignInManager<IdentityUserEntity>> signInManager
    )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<F1PasswordSignInResultModel> CheckPasswordSignInAsync(
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

    public async Task CreateRefreshTokenAsync(F1RefreshTokenModel model, CancellationToken ct)
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
    }

    public Task<bool> IsUserFoundByEmailAsync(string email, CancellationToken ct)
    {
        var upperCaseEmail = email.ToUpper();

        return _context
            .Set<IdentityUserEntity>()
            .AnyAsync(user => user.NormalizedEmail.Equals(upperCaseEmail), ct);
    }
}
