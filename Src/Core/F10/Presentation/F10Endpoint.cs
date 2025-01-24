using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F10.BusinessLogic;
using F10.Common;
using F10.Mapper;
using F10.Models;
using F10.Presentation.Filters.SetStateBag;
using F10.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F10.Presentation;

public sealed class F10Endpoint : ControllerBase
{
    private readonly F10Service _service;

    public F10Endpoint(F10Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for get list of todo list including pagination.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="404">TODO_TASK_LIST_NOT_FOUND</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F10Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpGet(F10Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F10SetStateBagFilter>]
    [ServiceFilter<F10ValidationFilter>]
    public async Task<IActionResult> ExecuteF10Async(
        [Required] F10Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F10AppRequestModel
        {
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F10HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
