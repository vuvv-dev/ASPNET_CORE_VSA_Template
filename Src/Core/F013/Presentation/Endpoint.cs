using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F013.BusinessLogic;
using F013.Common;
using F013.Mapper;
using F013.Models;
using F013.Presentation.Filters.SetStateBag;
using F013.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F013.Presentation;

[Tags(Constant.CONTROLLER_NAME)]
public sealed class Endpoint : ControllerBase
{
    private readonly Service _service;

    public Endpoint(Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for get uncompleted todo tasks including pagination.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="404">TASK_NOT_FOUND || TODO_TASK_LIST_NOT_FOUND</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(Response))]
    [Produces(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpGet(Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<SetStateBagFilter>]
    [ServiceFilter<ValidationFilter>]
    public async Task<IActionResult> ExecuteF013Async(
        [Required] Request request,
        CancellationToken ct
    )
    {
        var appRequest = new AppRequestModel
        {
            TodoTaskId = request.TodoTaskId,
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
