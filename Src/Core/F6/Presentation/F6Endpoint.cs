using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using F6.BusinessLogic;
using F6.Common;
using F6.Mapper;
using F6.Models;
using F6.Presentation.Filters.Authorization;
using F6.Presentation.Filters.SetStateBag;
using F6.Presentation.Filters.Validation;
using FCommon.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F6.Presentation;

public sealed class F6Endpoint : ControllerBase
{
    private readonly F6Service _service;

    public F6Endpoint(F6Service service)
    {
        _service = service;
    }

    /// <summary>
    ///     Endpoint for refresh access token function.
    /// </summary>
    /// <param name="request">
    ///     Incoming request.
    /// </param>
    /// <response code="400">VALIDATION_FAILED</response>
    /// <response code="500">SERVER_ERROR</response>
    /// <response code="404">REFRESH_TOKEN_DOES_NOT_EXIST</response>
    /// <response code="401">REFRESH_TOKEN_EXPIRED || UNAUTHORIZED</response>
    /// <response code="200">SUCCESS</response>
    /// <response code="403">FORBIDDEN</response>
    /// <response code="1">EXAMPLE RESPONSE OF ALL STATUS CODES</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(1, Type = typeof(F6Response))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    // =============================================================
    [HttpPost(F6Constant.ENDPOINT_PATH)]
    [Authorize(Policy = nameof(F6AuthorizationRequirement))]
    [ServiceFilter<F6SetStateBagFilter>]
    [ServiceFilter<F6ValidationFilter>]
    public async Task<IActionResult> ExecuteF6Async(
        [FromBody] [Required] F6Request request,
        CancellationToken ct
    )
    {
        var stateBag = HttpContext.Items[AppConstants.STATE_BAG_NAME] as F6StateBag;

        var appRequest = new F6AppRequestModel
        {
            AccessTokenId = stateBag.AccessTokenId,
            RefreshToken = request.RefreshToken,
            UserId = stateBag.UserId,
        };
        var appResponse = await _service.ExecuteAsync(appRequest, ct);

        var httpResponse = F6HttpResponseMapper.Get(appRequest, appResponse, HttpContext);

        return StatusCode(httpResponse.HttpCode, httpResponse);
    }
}
