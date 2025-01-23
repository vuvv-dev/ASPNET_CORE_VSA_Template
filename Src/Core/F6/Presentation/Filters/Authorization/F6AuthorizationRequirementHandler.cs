using System;
using System.Security.Claims;
using System.Threading.Tasks;
using F6.Presentation.Filters.SetStateBag;
using FCommon.AccessToken;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace F6.Presentation.Filters.Authorization;

public sealed class F6AuthorizationRequirementHandler
    : AuthorizationHandler<F6AuthorizationRequirement>
{
    private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;

    public F6AuthorizationRequirementHandler(Lazy<IHttpContextAccessor> httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        F6AuthorizationRequirement requirement
    )
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var expClaimValue = context.User.FindFirstValue(AppConstants.JsonWebToken.ClaimType.EXP);
        var isTokenExpired = AppAccessTokenHandler.IsAccessTokenExpired(expClaimValue);
        if (!isTokenExpired)
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var purposeClaimValue = context.User.FindFirstValue(
            AppConstants.JsonWebToken.ClaimType.PURPOSE.Name
        );
        if (
            !Equals(
                purposeClaimValue,
                AppConstants.JsonWebToken.ClaimType.PURPOSE.Value.USER_IN_APP
            )
        )
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var httpContext = _httpContextAccessor.Value.HttpContext;
        var stateBag = new F6StateBag
        {
            AccessTokenId = long.Parse(
                context.User.FindFirstValue(AppConstants.JsonWebToken.ClaimType.JTI)
            ),
            UserId = long.Parse(
                context.User.FindFirstValue(AppConstants.JsonWebToken.ClaimType.SUB)
            ),
        };
        httpContext.Items.Add(nameof(F6StateBag), stateBag);

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
