using System.Threading;
using System.Threading.Tasks;
using F10.BusinessLogic;
using F10.Common;
using F10.Mapper;
using F10.Models;
using F10.Presentation.Filters.SetStateBag;
using F10.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F10.Presentation;

public sealed class F10Endpoint : ControllerBase
{
    private readonly F10Service _service;

    public F10Endpoint(F10Service service)
    {
        _service = service;
    }

    [HttpGet(F10Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F10SetStateBagFilter>]
    [ServiceFilter<F10ValidationFilter>]
    public async Task<IActionResult> ExecuteAsync(F10Request request, CancellationToken ct)
    {
        var appRequest = new F10AppRequestModel
        {
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F10HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
