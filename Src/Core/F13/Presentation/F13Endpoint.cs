using System.Threading;
using System.Threading.Tasks;
using F13.BusinessLogic;
using F13.Common;
using F13.Mapper;
using F13.Models;
using F13.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F13.Presentation;

public sealed class F13Endpoint : ControllerBase
{
    private readonly F13Service _service;

    public F13Endpoint(F13Service service)
    {
        _service = service;
    }

    [HttpGet(F13Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F13ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F13Request request, CancellationToken ct)
    {
        var appRequest = new F13AppRequestModel();
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F13HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
