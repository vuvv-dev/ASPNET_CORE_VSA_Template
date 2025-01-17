using System.Threading;
using System.Threading.Tasks;
using F8.Src.BusinessLogic;
using F8.Src.Common;
using F8.Src.Mapper;
using F8.Src.Models;
using F8.Src.Presentation.Filters.Validation;
using FCommon.Src.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F8.Src.Presentation;

public sealed class F8Endpoint : ControllerBase
{
    private readonly F8Service _service;

    public F8Endpoint(F8Service service)
    {
        _service = service;
    }

    [HttpPost(F8Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F8ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F8Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F8AppRequestModel { };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F8HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
