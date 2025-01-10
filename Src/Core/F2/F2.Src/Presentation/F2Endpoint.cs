using System;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.BusinessLogic;
using F2.Src.Common;
using F2.Src.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> ExecuteAsync(F2Request request, CancellationToken ct)
    {
        F2Response response;

        var (appCode, todoTaskList) = await _service.Value.ExecuteAsync(request.ListId, ct);
        if (appCode != F2Constant.AppCode.SUCCESS)
        {
            response = new() { AppCode = appCode, Body = new() };

            return StatusCode(StatusCodes.Status404NotFound, response);
        }
        response = new F2Response
        {
            AppCode = appCode,
            Body = new()
            {
                TodoTaskList = new()
                {
                    Id = request.ListId,
                    Name = todoTaskList.Name,
                    CreatedDate = todoTaskList.CreatedDate,
                    UserId = todoTaskList.UserId,
                },
            },
        };

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
