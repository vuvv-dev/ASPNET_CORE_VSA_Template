using System;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;

namespace F5.Src.DataAccess;

public sealed class F5Repository : IF5Repository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public F5Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }
}
