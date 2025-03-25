using System;
using System.Security.Claims;
using System.Threading.Tasks;
using F006.Presentation.Filters.SetStateBag;
using FCommon.AccessToken;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace F006.Presentation.Filters.Authorization;

public sealed class AuthorizationRequirementHandler : AuthorizationHandler<AuthorizationRequirement>
{
    private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;

    public AuthorizationRequirementHandler(Lazy<IHttpContextAccessor> httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthorizationRequirement requirement
    )
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var expClaimValue = context.User.FindFirstValue(AppConstant.JsonWebToken.ClaimType.EXP);
        var isTokenExpired = AppAccessTokenHandler.IsAccessTokenExpired(expClaimValue);
        if (!isTokenExpired)
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var purposeClaimValue = context.User.FindFirstValue(
            AppConstant.JsonWebToken.ClaimType.PURPOSE.Name
        );
        if (
            !Equals(purposeClaimValue, AppConstant.JsonWebToken.ClaimType.PURPOSE.Value.USER_IN_APP)
        )
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var httpContext = _httpContextAccessor.Value.HttpContext;
        var stateBag = new StateBag
        {
            AccessTokenId = long.Parse(
                context.User.FindFirstValue(AppConstant.JsonWebToken.ClaimType.JTI)
            ),
            UserId = long.Parse(
                context.User.FindFirstValue(AppConstant.JsonWebToken.ClaimType.SUB)
            ),
        };
        httpContext.Items.Add(nameof(StateBag), stateBag);

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
