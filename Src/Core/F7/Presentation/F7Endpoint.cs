using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F7.BusinessLogic;
using F7.Common;
using F7.Mapper;
using F7.Models;
using F7.Presentation.Filters.SetStateBag;
using F7.Presentation.Filters.Validation;
using FCommon.Authorization.Default;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F7.Presentation;

public sealed class F7Endpoint : ControllerBase
{
    private readonly F7Service _service;

    public F7Endpoint(F7Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for create todo list.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="409">LIST_ALREADY_EXISTS</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F7Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F7Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(DefaultAuthorizationRequirement))]
    [ServiceFilter<F7SetStateBagFilter>]
    [ServiceFilter<F7ValidationFilter>]
    public async Task<IActionResult> ExecuteF7Async(
        [FromBody] [Required] F7Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F7AppRequestModel
        {
            TodoTaskListName = request.TodoTaskListName,
            UserId = long.Parse(
                HttpContext.Items[AppConstant.JsonWebToken.ClaimType.SUB] as string
            ),
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F7HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
