using System.Threading;
using System.Threading.Tasks;
using F15.BusinessLogic;
using F15.Common;
using F15.Mapper;
using F15.Models;
using F15.Presentation.Filters.SetStateBag;
using F15.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F15.Presentation;

public sealed class F15Endpoint : ControllerBase
{
    private readonly F15Service _service;

    public F15Endpoint(F15Service service)
    {
        _service = service;
    }

    [HttpGet(F15Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F15SetStateBagFilter>]
    [ServiceFilter<F15ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(F15Request request, CancellationToken ct)
    {
        var appRequest = new F15AppRequestModel { TodoTaskId = request.TodoTaskId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F15HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
