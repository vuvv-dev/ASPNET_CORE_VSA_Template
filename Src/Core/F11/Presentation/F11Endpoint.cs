using System.Threading;
using System.Threading.Tasks;
using F11.BusinessLogic;
using F11.Common;
using F11.Mapper;
using F11.Models;
using F11.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F11.Presentation;

public sealed class F11Endpoint : ControllerBase
{
    private readonly F11Service _service;

    public F11Endpoint(F11Service service)
    {
        _service = service;
    }

    [HttpPost(F11Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F11ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F11Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F11AppRequestModel
        {
            Content = request.Content,
            TodoTaskListId = request.TodoTaskListId,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F11HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
