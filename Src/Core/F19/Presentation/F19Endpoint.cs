using System.Threading;
using System.Threading.Tasks;
using F19.BusinessLogic;
using F19.Common;
using F19.Mapper;
using F19.Models;
using F19.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F19.Presentation;

public sealed class F19Endpoint : ControllerBase
{
    private readonly F19Service _service;

    public F19Endpoint(F19Service service)
    {
        _service = service;
    }

    [HttpPost(F19Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F19ValidationFilter>(Order = 1)]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F19Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F19AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            Content = request.Content,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F19HttpResponseMapper.Get(appRequest, appResponse);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
