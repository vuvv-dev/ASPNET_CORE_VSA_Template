using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F20.BusinessLogic;
using F20.Common;
using F20.Mapper;
using F20.Models;
using F20.Presentation.Filters.SetStateBag;
using F20.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F20.Presentation;

public sealed class F20Endpoint : ControllerBase
{
    private readonly F20Service _service;

    public F20Endpoint(F20Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for update todo task note.
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
    [ProducesResponseType(1, Type = typeof(F20Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F20Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F20SetStateBagFilter>]
    [ServiceFilter<F20ValidationFilter>]
    public async Task<IActionResult> ExecuteF20Async(
        [FromBody] [Required] F20Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F20AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            Note = request.Note,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F20HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
