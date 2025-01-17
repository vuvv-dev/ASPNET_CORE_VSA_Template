using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using F1.Src;
using F2.Src;
using F3.Src;
using F4.Src;
using F5.Src;
using F6.Src;
using F7.Src;
using FA1.Src;
using FA2.Src;
using FA3.Src;
using FACommon.Src.DependencyInjection;
using FCommon.Src;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entry.Src.Register;

public static class ServiceRegisterationCenter
{
    // Add assembly that you want to register HERE !!
    private static readonly List<Assembly> RegisterAssemblies =
    [
        // Core
        typeof(CommonServiceRegister).Assembly,
        typeof(F1Register).Assembly,
        typeof(F2Register).Assembly,
        typeof(F3Register).Assembly,
        typeof(F4Register).Assembly,
        typeof(F5Register).Assembly,
        typeof(F6Register).Assembly,
        typeof(F7Register).Assembly,
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
