using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FCommon.AccessToken;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FCommon.Authorization.Default;

public sealed class DefaultAuthorizationRequirementHandler
    : AuthorizationHandler<DefaultAuthorizationRequirement>
{
    private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;

    public DefaultAuthorizationRequirementHandler(Lazy<IHttpContextAccessor> httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DefaultAuthorizationRequirement requirement
    )
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var expClaimValue = context.User.FindFirstValue(AppConstant.JsonWebToken.ClaimType.EXP);
        var tokenExpireTime = AppAccessTokenHandler.IsAccessTokenExpired(expClaimValue);
        if (tokenExpireTime)
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
        httpContext.Items.Add(
            AppConstant.JsonWebToken.ClaimType.SUB,
            context.User.FindFirstValue(AppConstant.JsonWebToken.ClaimType.SUB)
        );

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
