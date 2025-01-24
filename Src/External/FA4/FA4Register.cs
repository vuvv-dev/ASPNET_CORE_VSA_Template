using System;
using FACommon.DependencyInjection;
using FConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace FA4;

public sealed class FA4Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureSwagger(services, configuration);

        return services;
    }

    private static IServiceCollection ConfigureSwagger(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        const string JWT_BEARER_SCHEME_NAME = "JWT Bearer";
        var swaggerOption = configuration
            .GetRequiredSection(key: "Swagger")
            .GetRequiredSection(key: "NSwag")
            .Get<NSwagOption>();

        services.AddOpenApiDocument(config =>
        {
            config.PostProcess = document =>
            {
                document.Info = new()
                {
                    Version = swaggerOption.Doc.PostProcess.Info.Version,
                    Title = swaggerOption.Doc.PostProcess.Info.Title,
                    Description = swaggerOption.Doc.PostProcess.Info.Description,
                    Contact = new()
                    {
                        Name = swaggerOption.Doc.PostProcess.Info.Contact.Name,
                        Email = swaggerOption.Doc.PostProcess.Info.Contact.Email,
                    },
                    License = new()
                    {
                        Name = swaggerOption.Doc.PostProcess.Info.License.Name,
                        Url = new(swaggerOption.Doc.PostProcess.Info.License.Url),
                    },
                };
            };

            config.AddSecurity(
                JWT_BEARER_SCHEME_NAME,
                new()
                {
                    Type = (OpenApiSecuritySchemeType)
                        Enum.ToObject(
                            typeof(OpenApiSecuritySchemeType),
                            swaggerOption.Doc.Auth.Bearer.Type
                        ),
                    In = (OpenApiSecurityApiKeyLocation)
                        Enum.ToObject(
                            typeof(OpenApiSecurityApiKeyLocation),
                            swaggerOption.Doc.Auth.Bearer.In
                        ),
                    Scheme = swaggerOption.Doc.Auth.Bearer.Scheme,
                    BearerFormat = swaggerOption.Doc.Auth.Bearer.BearerFormat,
                    Description = swaggerOption.Doc.Auth.Bearer.Description,
                }
            );

            config.OperationProcessors.Add(
                new OperationSecurityScopeProcessor(JWT_BEARER_SCHEME_NAME)
            );
        });

        return services;
    }
}
