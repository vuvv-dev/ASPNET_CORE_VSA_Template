using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using FACommon.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entry.Register;

public static class ServiceRegisterationCenter
{
    private static readonly Type ServiceRegisterType = typeof(IServiceRegister);

    public static async Task<IServiceCollection> RegisterRequiredServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var registerAssemblyNames = await GetListOfRegisteredAssemblyNameAsync();

        services = RegisterAssemblyByName(
            registerAssemblyNames.Assembly.External,
            services,
            configuration
        );

        services = RegisterAssemblyByName(
            registerAssemblyNames.Assembly.Core,
            services,
            configuration
        );

        services = RegisterAssemblyByName([nameof(Entry)], services, configuration);

        return services;
    }

    private static async Task<RegisteredAssemblyModel> GetListOfRegisteredAssemblyNameAsync()
    {
        const string AssemblyFileName = "app-assembly.json";

        var fullFilePath = Path.GetFullPath(
            Path.Combine(Environment.CurrentDirectory, "..", "..", AssemblyFileName)
        );
        var doesFileExist = File.Exists(fullFilePath);
        if (!doesFileExist)
        {
            throw new ApplicationException(
                $"No assembly file name {AssemblyFileName} is found in the current directory, please check !!"
            );
        }

        var json = await File.ReadAllTextAsync(fullFilePath);
        var result = JsonSerializer.Deserialize<RegisteredAssemblyModel>(json);

        return result;
    }

    private static IServiceCollection RegisterAssemblyByName(
        IEnumerable<string> assemblyNames,
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        foreach (var assemblyName in assemblyNames)
        {
            Assembly assembly;

            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (FileNotFoundException)
            {
                throw new ApplicationException(
                    $"No assembly {assemblyName} is found, please check !!"
                );
            }

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
