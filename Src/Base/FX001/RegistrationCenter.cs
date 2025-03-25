using System;
using Base.Common.DependencyInjection;
using Base.Config;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.FX001;

internal sealed class RegistrationCenter : IExternalServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        AddAppDbContextPool(services, configuration);
        AddAspNetCoreIdentity(services, configuration);

        return services;
    }

    private static void AddAppDbContextPool(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var configOption = configuration
            .GetRequiredSection("Database")
            .GetRequiredSection("Main")
            .Get<AppDbContextOption>();

        services.AddDbContextPool<AppDbContext>(
            config =>
            {
                config
                    .UseNpgsql(
                        configOption.ConnectionString,
                        dbOption =>
                        {
                            dbOption
                                .MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                                .EnableRetryOnFailure(configOption.EnableRetryOnFailure)
                                .CommandTimeout(configOption.CommandTimeOutInSecond);
                        }
                    )
                    .EnableSensitiveDataLogging(configOption.EnableSensitiveDataLogging)
                    .EnableDetailedErrors(configOption.EnableDetailedErrors)
                    .EnableThreadSafetyChecks(configOption.EnableThreadSafetyChecks)
                    .EnableServiceProviderCaching(configOption.EnableServiceProviderCaching);
            },
            configOption.MaxActiveConnections
        );
    }

    private static void AddAspNetCoreIdentity(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddIdentity<IdentityUserEntity, IdentityRoleEntity>(setupAction: config =>
            {
                var configOption = configuration
                    .GetRequiredSection("AspNetCoreIdentity")
                    .Get<AspNetCoreIdentityOption>();

                // Password configuration.
                config.Password.RequireDigit = configOption.Password.RequireDigit;
                config.Password.RequireLowercase = configOption.Password.RequireLowercase;
                config.Password.RequireNonAlphanumeric = configOption
                    .Password
                    .RequireNonAlphanumeric;
                config.Password.RequireUppercase = configOption.Password.RequireUppercase;
                config.Password.RequiredLength = configOption.Password.RequiredLength;
                config.Password.RequiredUniqueChars = configOption.Password.RequiredUniqueChars;

                // Lockout configuration.
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(
                    configOption.Lockout.DefaultLockoutTimeSpanInSecond
                );
                config.Lockout.MaxFailedAccessAttempts = configOption
                    .Lockout
                    .MaxFailedAccessAttempts;
                config.Lockout.AllowedForNewUsers = configOption.Lockout.AllowedForNewUsers;

                // User's credentials configuration.
                config.User.AllowedUserNameCharacters = config.User.AllowedUserNameCharacters;
                config.User.RequireUniqueEmail = config.User.RequireUniqueEmail;

                config.SignIn.RequireConfirmedEmail = configOption.SignIn.RequireConfirmedEmail;
                config.SignIn.RequireConfirmedPhoneNumber = configOption
                    .SignIn
                    .RequireConfirmedPhoneNumber;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}
