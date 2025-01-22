using System.Threading;
using System.Threading.Tasks;
using F20.BusinessLogic;
using F20.Common;
using F20.Mapper;
using F20.Models;
using F20.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F20.Presentation;

public sealed class F20Endpoint : ControllerBase
{
    private readonly F20Service _service;

    public F20Endpoint(F20Service service)
    {
        _service = service;
    }

    [HttpPost(F20Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F20ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F20Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F20AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            Note = request.Note,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F20HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
