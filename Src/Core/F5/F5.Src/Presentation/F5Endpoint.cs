using System.Threading;
using System.Threading.Tasks;
using F5.Src.BusinessLogic;
using F5.Src.Common;
using F5.Src.Mapper;
using F5.Src.Models;
using F5.Src.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F5.Src.Presentation;

public sealed class F5Endpoint : ControllerBase
{
    private readonly F5Service _service;

    public F5Endpoint(F5Service service)
    {
        _service = service;
    }

    [HttpPost(F5Constant.ENDPOINT_PATH)]
    [ServiceFilter<F5ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F5Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F5AppRequestModel { ResetPasswordToken = null };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F5HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
