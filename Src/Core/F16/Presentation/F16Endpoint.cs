using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F16.BusinessLogic;
using F16.Common;
using F16.Mapper;
using F16.Models;
using F16.Presentation.Filters.SetStateBag;
using F16.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F16.Presentation;

public sealed class F16Endpoint : ControllerBase
{
    private readonly F16Service _service;

    public F16Endpoint(F16Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for add or remove todo task from my day list.
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
    [ProducesResponseType(1, Type = typeof(F16Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F16Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F16SetStateBagFilter>]
    [ServiceFilter<F16ValidationFilter>]
    public async Task<IActionResult> ExecuteF16Async(
        [FromBody] [Required] F16Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F16AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsInMyDay = request.IsInMyDay,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F16HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
