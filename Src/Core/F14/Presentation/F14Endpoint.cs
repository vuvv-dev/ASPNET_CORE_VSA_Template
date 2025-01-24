using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F14.BusinessLogic;
using F14.Common;
using F14.Mapper;
using F14.Models;
using F14.Presentation.Filters.SetStateBag;
using F14.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F14.Presentation;

public sealed class F14Endpoint : ControllerBase
{
    private readonly F14Service _service;

    public F14Endpoint(F14Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for get completed todo tasks including pagination.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="404">TASK_NOT_FOUND || TODO_TASK_LIST_NOT_FOUND</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F14Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpGet(F14Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F14SetStateBagFilter>]
    [ServiceFilter<F14ValidationFilter>]
    public async Task<IActionResult> ExecuteF14Async(
        [Required] F14Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F14AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F14HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
