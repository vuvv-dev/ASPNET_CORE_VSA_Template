using System.Threading;
using System.Threading.Tasks;
using F9.BusinessLogic;
using F9.Common;
using F9.Mapper;
using F9.Models;
using F9.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F9.Presentation;

public sealed class F9Endpoint : ControllerBase
{
    private readonly F9Service _service;

    public F9Endpoint(F9Service service)
    {
        _service = service;
    }

    [HttpPost(F9Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F9ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F9Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F9AppRequestModel
        {
            NewName = request.NewName,
            TodoTaskListId = request.TodoTaskListId,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F9HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
