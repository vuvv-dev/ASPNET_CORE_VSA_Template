using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F19.BusinessLogic;
using F19.Common;
using F19.Mapper;
using F19.Models;
using F19.Presentation.Filters.SetStateBag;
using F19.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F19.Presentation;

public sealed class F19Endpoint : ControllerBase
{
    private readonly F19Service _service;

    public F19Endpoint(F19Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for update todo task content.
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
    [ProducesResponseType(1, Type = typeof(F19Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F19Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F19SetStateBagFilter>]
    [ServiceFilter<F19ValidationFilter>]
    public async Task<IActionResult> ExecuteF19Async(
        [FromBody] [Required] F19Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F19AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            Content = request.Content,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F19HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
