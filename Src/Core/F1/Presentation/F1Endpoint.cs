using System.Threading;
using System.Threading.Tasks;
using F1.BusinessLogic;
using F1.Common;
using F1.Mapper;
using F1.Models;
using F1.Presentation.Filters.SetStateBag;
using F1.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F1.Presentation;

public sealed class F1Endpoint : ControllerBase
{
    private readonly F1Service _service;

    public F1Endpoint(F1Service service)
    {
        _service = service;
    }

    [HttpPost(F1Constant.ENDPOINT_PATH)]
    [ServiceFilter<F1SetStateBagFilter>]
    [ServiceFilter<F1ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F1Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F1AppRequestModel
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F1HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
