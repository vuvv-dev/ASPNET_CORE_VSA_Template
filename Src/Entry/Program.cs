using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using Entry.Register;
using FACommon.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;

// Global Configuration.
Console.OutputEncoding = Encoding.UTF8;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// Setup services
var services = builder.Services;
var configuration = builder.Configuration;

await services.RegisterRequiredServices(configuration);

services.AddLogging(config =>
{
    config.ClearProviders().AddConsole();
});

services.AddControllers(config =>
{
    config.SuppressAsyncSuffixInActionNames = false;
});

services.AddHttpContextAccessor().MakeSingletonLazy<IHttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseOpenApi()
        .UseSwaggerUi(options =>
        {
            options.Path = string.Empty;
            options.DefaultModelsExpandDepth = -1;
        });

    app.MapControllers();
}

if (app.Environment.IsStaging())
{
    app.UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseOpenApi()
        .UseSwaggerUi(options =>
        {
            options.Path = string.Empty;
            options.DefaultModelsExpandDepth = -1;
        });

    app.MapControllers();
}

if (app.Environment.IsProduction())
{
    app.UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseOpenApi()
        .UseSwaggerUi(options =>
        {
            options.Path = string.Empty;
            options.DefaultModelsExpandDepth = -1;
        });

    app.MapControllers();
}

await app.RunAsync(CancellationToken.None);
