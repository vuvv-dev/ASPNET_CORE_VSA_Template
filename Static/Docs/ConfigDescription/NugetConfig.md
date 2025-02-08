# Understanding and Customizing `nuget.config`

This guide explains the purpose and structure of the `nuget.config` file, a crucial component for managing NuGet packages in your .NET projects.

**What is `nuget.config`?**

The `nuget.config` file is a configuration file used by NuGet, the package manager for .NET. It controls NuGet's behavior, including:

- **Package Sources:** Where NuGet looks for packages.
- **Package Installation Locations:** Where packages are installed.
- **Authentication:** Credentials for private package feeds.
- **Package Restore Behavior:** How and when packages are restored.
- **Security Scanning:** Sources to use to scan for vulnerabilities.

**Where is `nuget.config` located?**

`nuget.config` files can exist at multiple levels, with a hierarchical structure:

1.  **Machine-wide:** Affects all users on the system.
2.  **User-specific:** Affects only the current user.
3.  **Project-specific:** Affects only the specific solution or project.

NuGet combines settings from all levels, with project-specific settings taking precedence.

**Why define settings in `nuget.config`?**

While some settings might have defaults, explicitly defining them ensures consistency across all developer machines and build environments. This prevents issues caused by different default configurations and provides better control over package management.

**Customization:**

The `nuget.config` file is highly customizable. This guide covers the most common settings, but you can find more advanced options in the official Microsoft documentation: [nuget.config file](https://learn.microsoft.com/en-us/nuget/reference/nuget-config-file).

**Example `nuget.config` Structure:**

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageRestore>
    <add key="enabled" value="true" />
    <add key="automatic" value="true" />
  </packageRestore>

  <packageSources>
    <clear />  <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    </packageSources>

  <auditSources>
    <clear />  <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    </auditSources>
</configuration>
```

**Explanation of Settings:**

- **`<packageRestore>`:** Controls package restoration behavior.

  - `enabled="true"`: Enables package restore.
  - `automatic="true"`: Automatically restores missing packages during build.

- **`<packageSources>`:** Defines where NuGet looks for packages.

  - `<clear />`: **Crucially, this element clears any package sources inherited from higher-level `nuget.config` files.** This ensures that only the sources defined in this file are used. Without this, you could unintentionally be pulling packages from unexpected or unwanted sources.
  - `<add key="[Name]" value="https://www.faccts.de/docs/orca/5.0/tutorials/first_steps/trouble_install.html" />`: Adds a package source.
    - `key`: A descriptive name for the source (e.g., "nuget", "MyPrivateFeed").
    - `value`: The URL of the package feed (e.g., `https://api.nuget.org/v3/index.json` for NuGet.org) or a local file path.

- **`<auditSources>`:** Defines where NuGet looks for security vulnerability information about packages. \* `<clear />`: **Crucially, this element clears any audit sources inherited from higher-level `nuget.config` files.** This ensures that only the sources defined in this file are used.
  - `<add key="[Name]" value="https://www.faccts.de/docs/orca/5.0/tutorials/first_steps/trouble_install.html" />`: Adds an audit source.
    - `key`: A descriptive name for the source (e.g., "nuget").
    - `value`: The URL of the package feed (e.g., `https://api.nuget.org/v3/index.json` for NuGet.org) or a local file path. This is often the same as your `packageSources`.

## Again: There are more to customize for this file, but for me, this is enough.
