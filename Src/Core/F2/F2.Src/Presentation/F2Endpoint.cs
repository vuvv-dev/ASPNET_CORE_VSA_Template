using System;
using System.Threading.Tasks;
using F2.Src.BusinessLogic;
using F2.Src.Common;
using F2.Src.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;

namespace F2.Src.Presentation;

public sealed class F2Endpoint : ControllerBase
{
    private readonly Lazy<F2Service> _service;

    public F2Endpoint(Lazy<F2Service> service)
    {
        _service = service;
    }

    [HttpGet(F2Constant.ENDPOINT_PATH)]
    [ServiceFilter<F2ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F2Request request)
    {
        await Task.Delay(1);

        return Ok("Hello");
    }
}
