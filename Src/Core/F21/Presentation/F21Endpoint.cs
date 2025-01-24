using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F21.BusinessLogic;
using F21.Common;
using F21.Mapper;
using F21.Models;
using F21.Presentation.Filters.SetStateBag;
using F21.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F21.Presentation;

public sealed class F21Endpoint : ControllerBase
{
    private readonly F21Service _service;

    public F21Endpoint(F21Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for update todo task due date.
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
    [ProducesResponseType(1, Type = typeof(F21Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F21Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F21SetStateBagFilter>]
    [ServiceFilter<F21ValidationFilter>]
    public async Task<IActionResult> ExecuteF21Async(
        [FromBody] [Required] F21Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F21AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            DueDate = request.DueDate.ToUniversalTime(),
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F21HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
