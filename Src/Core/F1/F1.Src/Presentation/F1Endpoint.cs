using System;
using System.Threading;
using System.Threading.Tasks;
using F1.Src.BusinessLogic;
using F1.Src.Common;
using F1.Src.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1.Src.Presentation;

public sealed class F1Endpoint : ControllerBase
{
    private readonly Lazy<F1Service> _service;

    public F1Endpoint(Lazy<F1Service> service)
    {
        _service = service;
    }

    [HttpGet(F1Constant.ENDPOINT_PATH)]
    [ServiceFilter<F1ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F1Request request, CancellationToken ct)
    {
        F1Response response = new();

        await Task.Delay(1, ct);

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
