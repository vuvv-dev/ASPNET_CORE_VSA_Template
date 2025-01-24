using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F18.BusinessLogic;
using F18.Common;
using F18.Mapper;
using F18.Models;
using F18.Presentation.Filters.SetStateBag;
using F18.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F18.Presentation;

public sealed class F18Endpoint : ControllerBase
{
    private readonly F18Service _service;

    public F18Endpoint(F18Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for add or remove todo task from important list.
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
    [ProducesResponseType(1, Type = typeof(F18Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F18Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F18SetStateBagFilter>]
    [ServiceFilter<F18ValidationFilter>]
    public async Task<IActionResult> ExecuteF18Async(
        [FromBody] [Required] F18Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F18AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            IsImportant = request.IsImportant,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F18HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
