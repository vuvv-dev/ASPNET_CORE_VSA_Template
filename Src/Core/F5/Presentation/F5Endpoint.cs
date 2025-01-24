using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F5.BusinessLogic;
using F5.Common;
using F5.Mapper;
using F5.Models;
using F5.Presentation.Filters.Authorization;
using F5.Presentation.Filters.SetStateBag;
using F5.Presentation.Filters.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5.Presentation;

public sealed class F5Endpoint : ControllerBase
{
    private readonly F5Service _service;

    public F5Endpoint(F5Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for reset password function.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="404">TOKEN_DOES_NOT_EXIST</response>
    /// <response code="422">PASSWORD_IS_INVALID</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="401">UNAUTHORIZED</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F5Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F5Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(F5AuthorizationRequirement))]
    [ServiceFilter<F5SetStateBagFilter>]
    [ServiceFilter<F5ValidationFilter>]
    public async Task<IActionResult> ExecuteF5Async(
        [FromBody] [Required] F5Request request,
        CancellationToken ct
    )
    {
        var stateBag = HttpContext.Items[nameof(F5StateBag)] as F5StateBag;

        var appRequest = new F5AppRequestModel
        {
            ResetPasswordTokenId = stateBag.ResetPasswordTokenId,
            UserId = stateBag.UserId,
            NewPassword = request.NewPassword,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F5HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
