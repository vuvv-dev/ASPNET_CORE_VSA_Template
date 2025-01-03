using System;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.BusinessLogic;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace F2.Src.Presentation;

public sealed class F2Endpoint : Endpoint<F2Request, F2Response>
{
    private readonly Lazy<F2Service> _service;

    public F2Endpoint(Lazy<F2Service> service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get(F2Common.ENDPOINT_PATH);
        AllowAnonymous();
    }

    public override async Task<F2Response> ExecuteAsync(F2Request req, CancellationToken ct)
    {
        const string FailedMessage = "Failed";
        F2Response response;

        var (appCode, value) = _service.Value.Execute();

        if (appCode != F2Common.AppCode.SUCCESS)
        {
            response = new() { Message = FailedMessage };

            await SendAsync(response, StatusCodes.Status500InternalServerError, ct);

            return response;
        }

        response = new() { Message = $"Hello {req.Name} || value = {value}" };

        await SendAsync(response, StatusCodes.Status200OK, ct);

        return response;
    }
}
