# Understanding and Managing .NET Tools with `dotnet-tools.json`

This guide explains the purpose and structure of the `dotnet-tools.json` file, which is used to manage local .NET tools within a project.

**What is `dotnet-tools.json`?**

The `dotnet-tools.json` file is a configuration file that manages _local_ .NET tools. Local tools are installed for a specific project or solution, as opposed to _global_ tools which are installed system-wide. This file ensures that everyone working on the project uses the same versions of the required tools.

**Why use `dotnet-tools.json`?**

Consider a project that uses tools like `dotnet-ef` (Entity Framework Core tools) or code formatters. Without `dotnet-tools.json`:

- **Manual Installation:** Developers would need to manually install each tool.
- **Version Conflicts:** Different developers might use different tool versions, leading to inconsistencies.
- **Tracking Difficulty:** It's hard to keep track of which tools the project requires.

`dotnet-tools.json` solves these issues by:

- **Centralized Tool Definition:** The file lists all required tools and their versions.
- **Easy Restoration:** Developers can restore all tools with a single command (`dotnet tool restore`).
- **Version Consistency:** Ensures everyone uses the same tool versions.
- **Project-Specific Tools:** Tools are associated with the project, not the developer's machine.

**Customization:**

The `dotnet-tools.json` file is customizable. This guide covers the core elements, but more details can be found in the official Microsoft documentation: [Install a local tool](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool).

**Example `dotnet-tools.json` Structure:**

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

**Explanation of Settings:**

- **`version`:** The version of the `dotnet-tools.json` file format. This helps ensure compatibility.
- **`isRoot`:** A boolean value. `true` indicates this is the root `dotnet-tools.json` file for the project. This is important if you have nested projects.
- **`tools`:** This section lists the local tools. Each tool is defined as an object with the following properties:
  - **`[Tool Name]`:** The name of the tool (e.g., `csharpier`, `dotnet-ef`). This is the key in the `tools` object.
  - **`version`:** The _exact_ version of the tool to use. This is critical for consistency.
  - **`commands`:** An array of command names provided by the tool (e.g., `["dotnet-csharpier"]`, `["dotnet-ef"]`). This is how you invoke the tool from the command line.
  - **`rollForward`:** Controls how the tool handles updates. `false` (as in the example) means it will _not_ automatically roll forward to newer versions. This provides the most consistent behavior, which is typically what you want in a project.

## Again: There are more to customize for this file, but for me, this is enough.
