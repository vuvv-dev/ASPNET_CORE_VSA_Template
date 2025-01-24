using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F17.BusinessLogic;
using F17.Common;
using F17.Mapper;
using F17.Models;
using F17.Presentation.Filters.SetStateBag;
using F17.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F17.Presentation;

public sealed class F17Endpoint : ControllerBase
{
    private readonly F17Service _service;

    public F17Endpoint(F17Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for add or remove todo task from completed list.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="404">TASK_NOT_FOUND</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F17Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F17Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F17SetStateBagFilter>]
    [ServiceFilter<F17ValidationFilter>]
    public async Task<IActionResult> ExecuteF17Async(
        [FromBody] [Required] F17Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F17AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsCompleted = request.IsCompleted,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F17HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
