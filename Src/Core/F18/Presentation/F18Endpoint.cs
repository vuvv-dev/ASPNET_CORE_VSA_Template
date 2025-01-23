using System.Threading;
using System.Threading.Tasks;
using F18.BusinessLogic;
using F18.Common;
using F18.Mapper;
using F18.Models;
using F18.Presentation.Filters.SetStateBag;
using F18.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F18.Presentation;

public sealed class F18Endpoint : ControllerBase
{
    private readonly F18Service _service;

    public F18Endpoint(F18Service service)
    {
        _service = service;
    }

    [HttpPost(F18Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F18SetStateBagFilter>]
    [ServiceFilter<F18ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(
        [FromBody] F18Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F18AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsImportant = request.IsImportant,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F18HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
