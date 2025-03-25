using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Base.Common.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entry.Register;

internal static class AppServiceRegisterationCenter
{
    private static readonly Type ServiceRegisterType = typeof(IServiceRegister);

    internal static async Task<IServiceCollection> RegisterRequiredServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Entry
        services = RegistrationCenter.Register(services, configuration);

        services = Base.RegistrationCenter.Register(services, configuration);

        // Core
        var registerAssemblyNames = await GetListOfRegisteredAssemblyNameAsync();
        services = RegisterAssemblyByName(registerAssemblyNames, services, configuration);

        return services;
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
                    $"No classes that inherit {nameof(IServiceRegister)} interface are found in this assembly {assembly.GetName()}, please check again !!"
                );
            }
            if (isRegisterTypeFound > 1)
            {
                throw new ApplicationException(
                    $"Only 1 class that inherits {nameof(IServiceRegister)} interface can exist in this assembly {assembly.GetName()}, please check again !!"
                );
            }

            var registerType = allTypes.First(type =>
                ServiceRegisterType.IsAssignableFrom(type) && !type.IsInterface
            );
            var register = Activator.CreateInstance(registerType) as IServiceRegister;
            register.Register(services, configuration);
        }

        return services;
    }

    private static async Task<IEnumerable<string>> GetListOfRegisteredAssemblyNameAsync()
    {
        const string CsprojFile = "Entry.csproj";
        const string ProjectReferenceElementName = "ProjectReference";
        const string IncludeAttributeName = "Include";

        var fullFilePath = Path.GetFullPath(CsprojFile);
        var doesFileExist = File.Exists(fullFilePath);
        if (!doesFileExist)
        {
            throw new ApplicationException("Missing entry csproj !!");
        }

        using var stream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);

        var doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
        var projectNames = doc.Descendants(ProjectReferenceElementName)
            .Select(projectRef =>
                Path.GetFileNameWithoutExtension(projectRef.Attribute(IncludeAttributeName).Value)
            );

        return projectNames;
    }
}
