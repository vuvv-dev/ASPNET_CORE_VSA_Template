using System.Threading;
using System.Threading.Tasks;
using F14.BusinessLogic;
using F14.Common;
using F14.Mapper;
using F14.Models;
using F14.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F14.Presentation;

public sealed class F14Endpoint : ControllerBase
{
    private readonly F14Service _service;

    public F14Endpoint(F14Service service)
    {
        _service = service;
    }

    [HttpGet(F14Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F14ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F14Request request, CancellationToken ct)
    {
        var appRequest = new F14AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F14HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
