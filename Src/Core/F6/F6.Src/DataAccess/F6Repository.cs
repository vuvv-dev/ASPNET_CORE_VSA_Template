using System;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;

namespace F6.Src.DataAccess;

public sealed class F6Repository : IF6Repository
{
    private readonly AppDbContext _appContext;
    private readonly Lazy<UserManager<IdentityUserEntity>> _userManager;

    public F6Repository(AppDbContext context, Lazy<UserManager<IdentityUserEntity>> userManager)
    {
        _appContext = context;
        _userManager = userManager;
    }
}
