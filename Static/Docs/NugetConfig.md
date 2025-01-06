# 🛠️ nuget.config

| ⚡ TL;DR (quick version)                                                                                                                                                                                                                |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| The `nuget.config` file configures NuGet to automatically restore packages, fetch packages only from defined sources, and scan for vulnerabilities using defined sources, ensuring a streamlined and secure package management process. |

The `nuget.config` file is a configuration file used by NuGet, a package manager for .NET applications. It contains settings that control NuGet's behavior, such as the sources from which packages are retrieved, the locations where packages are installed, and authentication credentials for private feeds.

The `nuget.config` file can exist at multiple levels:

- Machine-wide configuration: Found in a standard location on your system, affecting all users.

- User-specific configuration: Located in the user's profile directory, affecting only that user's NuGet operations.

- Project-specific configuration: Found in a solution or project directory, overriding the above configurations for that specific project.

### How does it work

When resolving NuGet operations, NuGet combines settings from all `nuget.config` files (from machine-wide to project-specific) in a hierarchical manner, with project-specific settings taking precedence. It uses the XML format for defining settings.

### Why to define all of these when they are default

Well sometimes you cannot sure this is default in all dev machines. So clearly define them help all machine have the same setting when working with packages like the same package sources, same package restore behavior in all machines.

### Can you customize the file

There are more to customize for this file, you can checkout [nuget.config](https://learn.microsoft.com/en-us/nuget/reference/nuget-config-file) in microsoft documents to customize for more.

### Structure

Here’s the structure of the [nuget.config](/nuget.config) file in this project:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <!-- Behavior when restoring. -->
  <packageRestore>
    <add key="enabled" value="true" />
    <add key="automatic" value="true" />
  </packageRestore>

  <!-- Sources for retrieving packages. -->
  <packageSources>
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>

  <!-- Sources for scanning security vulnerabilities of packages. -->
  <auditSources>
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </auditSources>
</configuration>
```

### Explanation of Settings

`<packageRestore>`

This section configures the behavior of package restoration:

- enabled: Enables package restore for the project.
- automatic: Automatically restores missing packages when the project is built.

`<packageSources>`

Defines the sources from which NuGet packages are downloaded.

- The block should start with `<clear />` to clears any inherited package sources from higher-level configurations if this you want to be specific about the sources that you want this module / project to use.

- Each `<add>` element specifies:
  - key: The name of the source (e.g., nuget.org, PrivateRepo), any name you want.
  - value: The URL or local path to the package source.

`<auditSources>`

Specifies sources used for scanning package vulnerabilities.

- The block should start with `<clear />` to clears any inherited package sources from higher-level configurations if this you want to be specific about the sources that you want this module / project to use.

- Each `<add>` element specifies:
  - key: The name of the source (e.g., nuget.org, PrivateRepo), any name you want.
  - value: The URL or local path to the package source.

## Again: There are more to customize for this file, but for me, this is enough, you can checkout [nuget.config](https://learn.microsoft.com/en-us/nuget/reference/nuget-config-file) in microsoft documents to customize for more.
