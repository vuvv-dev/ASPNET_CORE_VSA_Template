# ⚙️ dotnet-tools.config

| ⚡ TL;DR (quick version)                                                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| The `dotnet-tools.json` file manages .NET tools installed in a project or solution, ensuring consistent tooling across environments. It tracks the tool names, versions, and installation sources to make it easy for developers to restore and use the same tools. |

The `dotnet-tools.json` file is a configuration file used in .NET projects to manage **local .NET tools**. These are command-line tools installed for a specific project or solution, rather than globally. The file tracks the tools and their settings, making it easy to restore and ensure consistency across development environments.

This is particularly useful in team projects or CI/CD pipelines where all contributors need the same set of tools.

### How does it work

Imagine a project requires a specific tool like `dotnet-ef` (Entity Framework CLI) for migrations. Without `dotnet-tools.json`:

- Developers need to manually install the tool.

- Version mismatches could cause errors or unexpected behavior.

- There's no easy way to track which tools are required for the project.

With `dotnet-tools.json`, the tools and their versions are defined centrally:

- Developers can restore tools with a single command (`dotnet tool restore`).

- The exact version and source are enforced, avoiding discrepancies.

- The tools become part of the project's environment, ensuring reproducibility.

### Can you customize the file

There are more to customize for this file, you can checkout [dotnet-tools.config](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool) in microsoft documents to customize for more.

### Structure

Here’s the structure of the [dotnet-tools.config](/.config/dotnet-tools.json) file in this project:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "csharpier": {
      "version": "0.30.4",
      "commands": ["dotnet-csharpier"],
      "rollForward": false
    },
    "dotnet-reportgenerator-globaltool": {
      "version": "5.4.2",
      "commands": ["reportgenerator"],
      "rollForward": false
    },
    "dotnet-ef": {
      "version": "9.0.0",
      "commands": ["dotnet-ef"],
      "rollForward": false
    }
  }
}
```

### Explanation of Settings

#### 1. Versioned Tool Management

The `"version"` property in the file ensures compatibility with the format of the `dotnet-tools.json` file itself.

#### 2. IsRoot Property

`"isRoot": true` marks this file as the root of the tool manifest.

#### 3. Tools Section

- Each key under `"tools"` is the name of a .NET tool (e.g., `dotnet-ef`, `csharpier`):

  - The "version" specifies the exact version of the tool to install.

  - The "commands" array lists the commands the tool provides.

  - The "rollForward" property controls how the tool handles updates.

## Again: There are more to customize for this file, but for me, this is enough, you can checkout [dotnet-tools.config](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool) in microsoft documents to customize for more.
