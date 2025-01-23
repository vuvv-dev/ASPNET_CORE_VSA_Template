using System.Threading;
using System.Threading.Tasks;
using F21.BusinessLogic;
using F21.Common;
using F21.Mapper;
using F21.Models;
using F21.Presentation.Filters.SetStateBag;
using F21.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F21.Presentation;

public sealed class F21Endpoint : ControllerBase
{
    private readonly F21Service _service;

    public F21Endpoint(F21Service service)
    {
        _service = service;
    }

    [HttpPost(F21Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F21SetStateBagFilter>]
    [ServiceFilter<F21ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F21Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F21AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            DueDate = request.DueDate.ToUniversalTime(),
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F21HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
