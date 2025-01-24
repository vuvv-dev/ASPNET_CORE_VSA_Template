using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F1.BusinessLogic;
using F1.Common;
using F1.Mapper;
using F1.Models;
using F1.Presentation.Filters.SetStateBag;
using F1.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1.Presentation;

public sealed class F1Endpoint : ControllerBase
{
    private readonly F1Service _service;

    public F1Endpoint(F1Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for user login.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="429">TEMPORARY_BANNED</response>
    /// <response code="401">PASSWORD_IS_INCORRECT</response>
    /// <response code="404">USER_NOT_FOUND</response>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(1, Type = typeof(F1Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F1Constant.ENDPOINT_PATH)]
    [ServiceFilter<F1SetStateBagFilter>]
    [ServiceFilter<F1ValidationFilter>]
    public async Task<IActionResult> ExecuteF1Async(
        [FromBody] [Required] F1Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F1AppRequestModel
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F1HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
