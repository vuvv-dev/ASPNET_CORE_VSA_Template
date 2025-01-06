# 🌍 global.json

| ⚡ TL;DR (quick version)                                                                                                                  |
| ----------------------------------------------------------------------------------------------------------------------------------------- |
| The `global.json` file in a .NET project pins the .NET SDK version to ensure consistent builds across development and CI/CD environments. |

The `global.json` file is a configuration file used in .NET projects to define specific settings for the development environment. Its primary purpose is to lock the .NET SDK version to ensure consistent behavior across different machines and build environments.

This is especially useful in teams or CI/CD pipelines, where mismatched SDK versions can cause unexpected issues.

### How does it work

Imagine a team of developers working on the same project, but each developer has a different version of the .NET SDK installed. Without `global.json`:

- Version Mismatch: Each developer might unknowingly use a different SDK version.

- Inconsistent Builds: Builds may behave differently depending on the environment.

- Compatibility Issues: Certain libraries or APIs might behave differently between SDK versions.

With `global.json`, the SDK version is explicitly specified, ensuring:

- The correct SDK version is used.

- Builds are consistent across environments.

- Compatibility is maintained throughout the project lifecycle.

### Can you customize the file

There are more to customize for this file, you can checkout [global.json](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json) in microsoft documents to customize for more.

### Structure

Here’s the structure of the [global.json](/global.json) file in this project:

```json
{
  "sdk": {
    "version": "8.0.404",
    "rollForward": "disable",
    "allowPrerelease": false
  }
}
```

### Explanation of Settings

`sdk`

Defines the specific version of the .NET SDK to use:

- `version`: Specifies the exact version.

- `rollForward`: Determines how the SDK handles updates.

- `allowPrerelease`: Allows the use of prerelease SDK versions.

## Again: There are more to customize for this file, but for me, this is enough, you can checkout [global.json](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json) in microsoft documents to customize for more.
