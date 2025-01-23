using System.Threading;
using System.Threading.Tasks;
using F4.BusinessLogic;
using F4.Common;
using F4.Mapper;
using F4.Models;
using F4.Presentation.Filters.SetStateBag;
using F4.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F4.Presentation;

public sealed class F4Endpoint : ControllerBase
{
    private readonly F4Service _service;

    public F4Endpoint(F4Service service)
    {
        _service = service;
    }

    [HttpPost(F4Constant.ENDPOINT_PATH)]
    [ServiceFilter<F4SetStateBagFilter>]
    [ServiceFilter<F4ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F4Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F4AppRequestModel { Email = request.Email };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F4HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
