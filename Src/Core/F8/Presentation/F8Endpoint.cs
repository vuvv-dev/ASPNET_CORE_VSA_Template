using System.Threading;
using System.Threading.Tasks;
using F8.BusinessLogic;
using F8.Common;
using F8.Mapper;
using F8.Models;
using F8.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F8.Presentation;

public sealed class F8Endpoint : ControllerBase
{
    private readonly F8Service _service;

    public F8Endpoint(F8Service service)
    {
        _service = service;
    }

    [HttpDelete(F8Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F8ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(F8Request request, CancellationToken ct)
    {
        var appRequest = new F8AppRequestModel { TodoTaskListId = request.TodoTaskListId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F8HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
