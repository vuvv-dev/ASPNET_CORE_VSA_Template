using System.Threading;
using System.Threading.Tasks;
using F2.Src.BusinessLogic;
using F2.Src.Common;
using F2.Src.Mapper;
using F2.Src.Models;
using F2.Src.Presentation.Filters.Validation;
using FCommon.Src.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F2.Src.Presentation;

public sealed class F2Endpoint : ControllerBase
{
    private readonly F2Service _service;

    public F2Endpoint(F2Service service)
    {
        _service = service;
    }

    [HttpGet(F2Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F2ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F2Request request, CancellationToken ct)
    {
        var appRequest = new F2AppRequestModel { ListId = request.ListId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F2HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
