using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F3.BusinessLogic;
using F3.Common;
using F3.Mapper;
using F3.Models;
using F3.Presentation.Filters.SetStateBag;
using F3.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F3.Presentation;

public sealed class F3Endpoint : ControllerBase
{
    private readonly F3Service _service;

    public F3Endpoint(F3Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for register new user.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="422">PASSWORD_IS_INVALID</response>
    /// <response code="409">EMAIL_ALREADY_EXISTS</response>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(1, Type = typeof(F3Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F3Constant.ENDPOINT_PATH)]
    [ServiceFilter<F3SetStateBagFilter>]
    [ServiceFilter<F3ValidationFilter>]
    public async Task<IActionResult> ExecuteF3Async(
        [FromBody] [Required] F3Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F3AppRequestModel
        {
            Email = request.Email,
            Password = request.Password,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F3HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
