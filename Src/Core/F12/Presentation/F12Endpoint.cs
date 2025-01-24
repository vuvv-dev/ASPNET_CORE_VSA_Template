using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F12.BusinessLogic;
using F12.Common;
using F12.Mapper;
using F12.Models;
using F12.Presentation.Filters.SetStateBag;
using F12.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F12.Presentation;

public sealed class F12Endpoint : ControllerBase
{
    private readonly F12Service _service;

    public F12Endpoint(F12Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for remove todo task.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="404">TODO_TASK_NOT_FOUND</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F12Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpDelete(F12Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F12SetStateBagFilter>]
    [ServiceFilter<F12ValidationFilter>]
    public async Task<IActionResult> ExecuteF12Async(
        [Required] F12Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F12AppRequestModel { TodoTaskId = request.TodoTaskId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F12HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
