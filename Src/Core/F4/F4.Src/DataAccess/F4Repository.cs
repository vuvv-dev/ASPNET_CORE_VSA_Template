using System;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;

namespace F4.Src.DataAccess;

public sealed class F4Repository : IF4Repository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public F4Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }
}
