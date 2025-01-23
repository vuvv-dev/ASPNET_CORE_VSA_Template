using System.Threading;
using System.Threading.Tasks;
using F16.BusinessLogic;
using F16.Common;
using F16.Mapper;
using F16.Models;
using F16.Presentation.Filters.SetStateBag;
using F16.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F16.Presentation;

public sealed class F16Endpoint : ControllerBase
{
    private readonly F16Service _service;

    public F16Endpoint(F16Service service)
    {
        _service = service;
    }

    [HttpPost(F16Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F16SetStateBagFilter>]
    [ServiceFilter<F16ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F16Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F16AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsInMyDay = request.IsInMyDay,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F16HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
