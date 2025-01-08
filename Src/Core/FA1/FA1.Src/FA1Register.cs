using System;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using FCommon.Src.DependencyInjection;
using FConfig.Src;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FA1.Src;

public sealed class FA1Register : IServiceRegister
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
        services.AddDbContextPool<AppDbContext>(config =>
        {
            var configOption = configuration
                .GetRequiredSection("Database")
                .GetRequiredSection("MainDb")
                .Get<AppDbContextOption>();

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
        });
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
                    .Get<AspNetCoreIdentityOptions>();

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
                    value: configOption.Lockout.DefaultLockoutTimeSpanInSecond
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
