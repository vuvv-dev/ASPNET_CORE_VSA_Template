using System.Threading;
using System.Threading.Tasks;
using F5.BusinessLogic;
using F5.Common;
using F5.Mapper;
using F5.Models;
using F5.Presentation.Filters.Authorization;
using F5.Presentation.Filters.Validation;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F5.Presentation;

public sealed class F5Endpoint : ControllerBase
{
    private readonly F5Service _service;

    public F5Endpoint(F5Service service)
    {
        _service = service;
    }

    [HttpPost(F5Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(F5AuthorizationRequirement))]
    [ServiceFilter<F5ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F5Request request,
        CancellationToken ct
    )
    {
        var stateBag = HttpContext.Items[AppConstants.STATE_BAG_NAME] as F5StateBag;

        var appRequest = new F5AppRequestModel
        {
            ResetPasswordTokenId = stateBag.ResetPasswordTokenId,
            UserId = stateBag.UserId,
            NewPassword = request.NewPassword,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F5HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
