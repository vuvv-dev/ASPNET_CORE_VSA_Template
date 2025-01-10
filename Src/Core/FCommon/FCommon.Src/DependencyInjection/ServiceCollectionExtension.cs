using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FCommon.Src.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection MakeSingletonLazy<T>(this IServiceCollection services)
        where T : class
    {
        return services.AddSingleton<Lazy<T>>(provider => new(provider.GetRequiredService<T>()));
    }

    public static IServiceCollection MakeScopedLazy<T>(this IServiceCollection services)
        where T : class
    {
        return services.AddScoped<Lazy<T>>(provider => new(provider.GetRequiredService<T>()));
    }

    // Please pass in assembly type, otherwise, there will be
    // exception
    public static IServiceCollection RegisterFiltersFromAssembly(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var actionFilterType = typeof(IAsyncActionFilter);
        var allTypes = assembly.GetTypes();

        var isFilterFound = allTypes.Any(type =>
            actionFilterType.IsAssignableFrom(type) && !type.IsInterface
        );
        if (!isFilterFound)
        {
            throw new ApplicationException(
                $"No filters are found in this assembly {assembly.GetName()}, please omit this function !!"
            );
        }

        foreach (var type in allTypes)
        {
            // Is this type implement interface and not the interface
            // itself
            if (actionFilterType.IsAssignableFrom(type) && !type.IsInterface)
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }
}
