# Understanding and Using `global.json`

This guide explains the purpose and structure of the `global.json` file in .NET projects, focusing on its role in ensuring consistent builds.

**What is `global.json`?**

The `global.json` file is a configuration file that allows you to define settings for the .NET SDK used in your project. Its primary purpose is to _pin_ or _lock_ the .NET SDK version, ensuring that everyone working on the project uses the same SDK version, regardless of what they have installed locally. This is vital for consistent builds across development machines, continuous integration (CI) servers, and other environments.

**Why use `global.json`?**

Imagine a team of developers. Without `global.json`, each developer might have a different .NET SDK version installed. This can lead to:

- **Version Mismatches:** Developers unknowingly use different SDK versions.
- **Inconsistent Builds:** Builds produce different results depending on the environment.
- **Compatibility Issues:** Code that works for one developer might break for another due to SDK differences.

`global.json` solves these problems by explicitly specifying the SDK version, guaranteeing:

- **Consistent SDK Usage:** Everyone uses the same SDK version.
- **Consistent Builds:** Builds produce the same results across all environments.
- **Improved Compatibility:** Reduces the risk of compatibility issues due to SDK version differences.

**Customization:**

While `global.json` primarily focuses on SDK version management, it does offer some customization options. This guide covers the essentials, but you can explore more advanced features in the official Microsoft documentation: [global.json](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json).

**Example `global.json` Structure:**

```json
{
  "sdk": {
    "version": "8.0.404",
    "rollForward": "disable",
    "allowPrerelease": false
  }
}
```

**Explanation of Settings:**

- **`sdk`:** This section defines the .NET SDK settings.
  - **`version`:** Specifies the _exact_ .NET SDK version to use (e.g., "8.0.404"). This is the most important setting.
  - **`rollForward`:** Determines how the SDK should handle situations where the specified `version` is not found. Common values include:
    - `latestMajor`: Roll forward to the latest major version.
    - `latestMinor`: Roll forward to the latest minor version.
    - `latestPatch`: Roll forward to the latest patch version.
    - `disable`: Do not roll forward. This is often preferred for production to ensure maximum consistency.
  - **`allowPrerelease`:** A boolean value. `true` allows using prerelease (e.g., preview, alpha, beta) SDK versions. Generally, set this to `false` for production projects.

## Again: There are more to customize for this file, but for me, this is enough.
