using System.Threading;
using System.Threading.Tasks;
using F4.Src.BusinessLogic;
using F4.Src.Common;
using F4.Src.Mapper;
using F4.Src.Models;
using F4.Src.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F4.Src.Presentation;

public sealed class F4Endpoint : ControllerBase
{
    private readonly F4Service _service;

    public F4Endpoint(F4Service service)
    {
        _service = service;
    }

    [HttpPost(F4Constant.ENDPOINT_PATH)]
    [ServiceFilter<F4ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F4Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F4AppRequestModel { Email = request.Email };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F4HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
