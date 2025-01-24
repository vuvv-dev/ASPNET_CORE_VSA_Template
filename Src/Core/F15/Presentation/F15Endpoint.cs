using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F15.BusinessLogic;
using F15.Common;
using F15.Mapper;
using F15.Models;
using F15.Presentation.Filters.SetStateBag;
using F15.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F15.Presentation;

public sealed class F15Endpoint : ControllerBase
{
    private readonly F15Service _service;

    public F15Endpoint(F15Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for get todo task details.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="404">TASK_NOT_FOUND</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F15Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpGet(F15Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F15SetStateBagFilter>]
    [ServiceFilter<F15ValidationFilter>]
    public async Task<IActionResult> ExecuteF15Async(
        [Required] F15Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F15AppRequestModel { TodoTaskId = request.TodoTaskId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F15HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
