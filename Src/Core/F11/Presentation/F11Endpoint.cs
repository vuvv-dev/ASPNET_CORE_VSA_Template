using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F11.BusinessLogic;
using F11.Common;
using F11.Mapper;
using F11.Models;
using F11.Presentation.Filters.SetStateBag;
using F11.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F11.Presentation;

public sealed class F11Endpoint : ControllerBase
{
    private readonly F11Service _service;

    public F11Endpoint(F11Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for create todo task.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="404">TODO_TASK_LIST_NOT_FOUND</response>
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
    [ProducesResponseType(1, Type = typeof(F11Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F11Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F11ValidationFilter>]
    [ServiceFilter<F11SetStateBagFilter>]
    public async Task<IActionResult> ExecuteF11Async(
        [FromBody] [Required] F11Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F11AppRequestModel
        {
            Content = request.Content,
            TodoTaskListId = request.TodoTaskListId,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F11HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
