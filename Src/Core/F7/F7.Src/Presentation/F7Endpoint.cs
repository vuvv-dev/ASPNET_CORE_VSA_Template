using System.Threading;
using System.Threading.Tasks;
using F7.Src.BusinessLogic;
using F7.Src.Common;
using F7.Src.Mapper;
using F7.Src.Models;
using F7.Src.Presentation.Filters.Validation;
using FCommon.Src.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7.Src.Presentation;

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
        var appRequest = new F7AppRequestModel { };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F7HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
