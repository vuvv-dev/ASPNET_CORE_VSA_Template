using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F8.BusinessLogic;
using F8.Common;
using F8.Mapper;
using F8.Models;
using F8.Presentation.Filters.SetStateBag;
using F8.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F8.Presentation;

public sealed class F8Endpoint : ControllerBase
{
    private readonly F8Service _service;

    public F8Endpoint(F8Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for remove todo list.
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
    [ProducesResponseType(1, Type = typeof(F8Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpDelete(F8Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F8SetStateBagFilter>]
    [ServiceFilter<F8ValidationFilter>]
    public async Task<IActionResult> ExecuteF8Async(
        [Required] F8Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F8AppRequestModel { TodoTaskListId = request.TodoTaskListId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F8HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
