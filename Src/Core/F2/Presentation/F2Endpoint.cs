using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F2.BusinessLogic;
using F2.Common;
using F2.Mapper;
using F2.Models;
using F2.Presentation.Filters.SetStateBag;
using F2.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F2.Presentation;

public sealed class F2Endpoint : ControllerBase
{
    private readonly F2Service _service;

    public F2Endpoint(F2Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for getting todo list detail by id.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="404">LIST_NOT_FOUND</response>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F2Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpGet(F2Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F2SetStateBagFilter>]
    [ServiceFilter<F2ValidationFilter>]
    public async Task<IActionResult> ExecuteF2Async(
        [Required] F2Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F2AppRequestModel { TodoTaskListId = request.TodoTaskListId };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F2HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
