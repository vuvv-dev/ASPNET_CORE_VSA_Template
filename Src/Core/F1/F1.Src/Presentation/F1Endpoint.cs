using System.Threading;
using System.Threading.Tasks;
using F1.Src.BusinessLogic;
using F1.Src.Common;
using F1.Src.Mapper;
using F1.Src.Models;
using F1.Src.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F1.Src.Presentation;

public sealed class F1Endpoint : ControllerBase
{
    private readonly F1Service _service;

    public F1Endpoint(F1Service service)
    {
        _service = service;
    }

    [HttpGet(F1Constant.ENDPOINT_PATH)]
    [ServiceFilter<F1ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F1Request request, CancellationToken ct)
    {
        var appRequest = new F1AppRequestModel
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F1HttpResponseMapper.Get(appResponse.AppCode);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
