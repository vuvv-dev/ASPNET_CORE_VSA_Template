using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F9.BusinessLogic;
using F9.Common;
using F9.Mapper;
using F9.Models;
using F9.Presentation.Filters.SetStateBag;
using F9.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F9.Presentation;

public sealed class F9Endpoint : ControllerBase
{
    private readonly F9Service _service;

    public F9Endpoint(F9Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for update todo list.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="404">LIST_DOES_NOT_EXIST</response>
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
    [ProducesResponseType(1, Type = typeof(F9Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F9Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F9SetStateBagFilter>]
    [ServiceFilter<F9ValidationFilter>]
    public async Task<IActionResult> ExecuteF9Async(
        [FromBody] [Required] F9Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F9AppRequestModel
        {
            NewName = request.NewName,
            TodoTaskListId = request.TodoTaskListId,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F9HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
