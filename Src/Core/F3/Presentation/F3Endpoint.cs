using System.Threading;
using System.Threading.Tasks;
using F3.BusinessLogic;
using F3.Common;
using F3.Mapper;
using F3.Models;
using F3.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Mvc;

namespace F3.Presentation;

public sealed class F3Endpoint : ControllerBase
{
    private readonly F3Service _service;

    public F3Endpoint(F3Service service)
    {
        _service = service;
    }

    [HttpPost(F3Constant.ENDPOINT_PATH)]
    [ServiceFilter<F3ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F3Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F3AppRequestModel
        {
            Email = request.Email,
            Password = request.Password,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F3HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
