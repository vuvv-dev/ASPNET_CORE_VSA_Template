using System.Threading;
using System.Threading.Tasks;
using F7.BusinessLogic;
using F7.Common;
using F7.Mapper;
using F7.Models;
using F7.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7.Presentation;

public sealed class F7Endpoint : ControllerBase
{
    private readonly F7Service _service;

    public F7Endpoint(F7Service service)
    {
        _service = service;
    }

    [HttpPost(F7Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F7ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F7Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F7AppRequestModel
        {
            TodoTaskListName = request.TodoTaskListName,
            UserId = long.Parse(
                HttpContext.Items[AppConstants.JsonWebToken.ClaimType.SUB] as string
            ),
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F7HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
