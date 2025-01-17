using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FCommon.Src.AccessToken;
using FCommon.Src.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FCommon.Src.Authorization.Default;

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
        var expClaimValue = context.User.FindFirstValue(AppConstants.JsonWebToken.ClaimType.EXP);
        var tokenExpireTime = AppAccessTokenHandler.IsAccessTokenExpired(expClaimValue);
        if (tokenExpireTime)
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
        httpContext.Items.Add(
            AppConstants.JsonWebToken.ClaimType.SUB,
            context.User.FindFirstValue(AppConstants.JsonWebToken.ClaimType.SUB)
        );

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
