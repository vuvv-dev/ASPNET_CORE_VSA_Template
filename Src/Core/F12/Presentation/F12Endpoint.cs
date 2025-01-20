using System.Threading;
using System.Threading.Tasks;
using F12.BusinessLogic;
using F12.Common;
using F12.Mapper;
using F12.Models;
using F12.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F12.Presentation;

public sealed class F12Endpoint : ControllerBase
{
    private readonly F12Service _service;

    public F12Endpoint(F12Service service)
    {
        _service = service;
    }

    [HttpDelete(F12Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F12ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F12Request request, CancellationToken ct)
    {
        var appRequest = new F12AppRequestModel { TodoTaskId = request.TodoTaskId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F12HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
