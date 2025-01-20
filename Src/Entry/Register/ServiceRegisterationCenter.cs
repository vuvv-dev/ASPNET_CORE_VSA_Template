using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using F1;
using F10;
using F11;
using F12;
using F13;
using F2;
using F3;
using F4;
using F5;
using F6;
using F7;
using F8;
using F9;
using FA1;
using FA2;
using FA3;
using FACommon.DependencyInjection;
using FCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entry.Register;

public static class ServiceRegisterationCenter
{
    // Add assembly that you want to register HERE !!
    private static readonly List<Assembly> RegisterAssemblies =
    [
        // Common
        typeof(CommonServiceRegister).Assembly,
        // Core
        typeof(F1Register).Assembly,
        typeof(F2Register).Assembly,
        typeof(F3Register).Assembly,
        typeof(F4Register).Assembly,
        typeof(F5Register).Assembly,
        typeof(F6Register).Assembly,
        typeof(F7Register).Assembly,
        typeof(F8Register).Assembly,
        typeof(F9Register).Assembly,
        typeof(F10Register).Assembly,
        typeof(F10Register).Assembly,
        typeof(F11Register).Assembly,
        typeof(F12Register).Assembly,
        typeof(F13Register).Assembly,
        // External
        typeof(FA1Register).Assembly,
        typeof(FA2Register).Assembly,
        typeof(FA3Register).Assembly,
    ];
    private static readonly Type ServiceRegisterType = typeof(IServiceRegister);

    public static IServiceCollection RegisterRequiredServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        foreach (var assembly in RegisterAssemblies)
        {
            var allTypes = assembly.GetTypes();

            var isRegisterTypeFound = allTypes.Count(type =>
                ServiceRegisterType.IsAssignableFrom(type) && !type.IsInterface
            );
            if (isRegisterTypeFound < 1)
            {
                throw new ApplicationException(
                    $"No register types are found in this assembly {assembly.GetName()}, please add one !!"
                );
            }
            if (isRegisterTypeFound > 1)
            {
                throw new ApplicationException(
                    $"Only 1 register type can be existed in this assembly {assembly.GetName()}, please remove one !!"
                );
            }

            foreach (var type in allTypes)
            {
                if (ServiceRegisterType.IsAssignableFrom(type) && !type.IsInterface)
                {
                    var register = Activator.CreateInstance(type) as IServiceRegister;
                    register.Register(services, configuration);

                    break;
                }
            }
        }

        return services;
    }
}
