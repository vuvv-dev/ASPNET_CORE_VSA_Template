using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F4.BusinessLogic;
using F4.Common;
using F4.Mapper;
using F4.Models;
using F4.Presentation.Filters.SetStateBag;
using F4.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F4.Presentation;

public sealed class F4Endpoint : ControllerBase
{
    private readonly F4Service _service;

    public F4Endpoint(F4Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for forgot password function.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="404">USER_NOT_FOUND</response>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(1, Type = typeof(F4Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F4Constant.ENDPOINT_PATH)]
    [ServiceFilter<F4SetStateBagFilter>]
    [ServiceFilter<F4ValidationFilter>]
    public async Task<IActionResult> ExecuteF4Async(
        [FromBody] [Required] F4Request request,
        CancellationToken ct
    )
    {
        var appRequest = new F4AppRequestModel { Email = request.Email };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F4HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
