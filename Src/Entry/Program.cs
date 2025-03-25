using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using Entry.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
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
    app.UseRouting().UseAuthentication().UseAuthorization();

    app.MapControllers();
}

if (app.Environment.IsProduction())
{
    app.UseRouting().UseAuthentication().UseAuthorization();

    app.MapControllers();
}

await app.RunAsync(CancellationToken.None);
