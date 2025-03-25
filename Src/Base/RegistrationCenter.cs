using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Base.Common.DependencyInjection;
using Base.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Base;

public static class RegistrationCenter
{
    public static IServiceCollection Register(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddExteralServices(services, configuration);
        AddOptions(services, configuration);

        return services;
    }

    private static IServiceCollection AddExteralServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var externalServiceRegisterType = typeof(IExternalServiceRegister);
        var currentAssembly = typeof(RegistrationCenter).Assembly;

        // Scan all
        var allTypes = currentAssembly.GetTypes();

        var areExternalServicesFound = allTypes.Any(type =>
            externalServiceRegisterType.IsAssignableFrom(type) && !type.IsInterface
        );
        if (!areExternalServicesFound)
        {
            throw new ApplicationException(
                $"No Registration of external modules are found in this assembly {currentAssembly.GetName()}, please check again !!"
            );
        }

        foreach (var type in allTypes)
        {
            if (externalServiceRegisterType.IsAssignableFrom(type) && !type.IsInterface)
            {
                var register = Activator.CreateInstance(type) as IExternalServiceRegister;
                register.Register(services, configuration);
            }
        }

        return services;
    }

    private static IServiceCollection AddOptions(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var appAuthOption = configuration
            .GetRequiredSection("Authentication")
            .GetRequiredSection("Jwt")
            .GetRequiredSection("User")
            .Get<JwtAuthenticationOption>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = appAuthOption.ValidateIssuer,
            ValidateAudience = appAuthOption.ValidateAudience,
            ValidateLifetime = appAuthOption.ValidateLifetime,
            ValidateIssuerSigningKey = appAuthOption.ValidateIssuerSigningKey,
            RequireExpirationTime = appAuthOption.RequireExpirationTime,
            ValidTypes = appAuthOption.ValidTypes,
            ValidIssuer = appAuthOption.ValidIssuer,
            ValidAudience = appAuthOption.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                new HMACSHA256(Encoding.UTF8.GetBytes(appAuthOption.IssuerSigningKey)).Key
            ),
        };

        services.AddSingleton(tokenValidationParameters);

        var aspNetCoreIdentityOption = configuration
            .GetRequiredSection("AspNetCoreIdentity")
            .Get<AspNetCoreIdentityOption>();

        services.AddSingleton(aspNetCoreIdentityOption);

        return services;
    }
}
