using System.Threading;
using Entry.Src.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Setup services
var services = builder.Services;
var configuration = builder.Configuration;

services.RegisterRequiredServices(configuration);

services.AddLogging(config =>
{
    config.ClearProviders().AddConsole();
});

services.AddControllers(config =>
{
    config.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRouting();

    app.MapControllers();
}

if (app.Environment.IsStaging())
{
    app.UseRouting();

    app.MapControllers();
}

if (app.Environment.IsProduction())
{
    app.UseRouting();

    app.MapControllers();
}

await app.RunAsync(CancellationToken.None);
