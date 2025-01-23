using System.Threading;
using System.Threading.Tasks;
using F17.BusinessLogic;
using F17.Common;
using F17.Mapper;
using F17.Models;
using F17.Presentation.Filters.SetStateBag;
using F17.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F17.Presentation;

public sealed class F17Endpoint : ControllerBase
{
    private readonly F17Service _service;

    public F17Endpoint(F17Service service)
    {
        _service = service;
    }

    [HttpPost(F17Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F17SetStateBagFilter>]
    [ServiceFilter<F17ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F17Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F17AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsCompleted = request.IsCompleted,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F17HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
