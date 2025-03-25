using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Common.DependencyInjection;

public static class ServiceCollectionExtension
{
    private static readonly Type AsyncActionFilterType = typeof(IAsyncActionFilter);

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
        var allTypes = assembly.GetTypes();

        var areFiltersFound = allTypes.Any(type =>
            AsyncActionFilterType.IsAssignableFrom(type) && !type.IsInterface
        );
        if (!areFiltersFound)
        {
            throw new ApplicationException(
                $"No filters are found in this assembly {assembly.GetName()}, please omit this function !!"
            );
        }

        foreach (var type in allTypes)
        {
            // Is this type implement interface and not the interface
            // itself
            if (AsyncActionFilterType.IsAssignableFrom(type) && !type.IsInterface)
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }
}
