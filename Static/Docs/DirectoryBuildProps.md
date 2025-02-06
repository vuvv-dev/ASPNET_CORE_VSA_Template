# Streamlining Builds with `Directory.Build.props`

This guide explains the purpose and structure of the `Directory.Build.props` file, a powerful MSBuild feature for managing common build settings across a .NET solution.

**What is `Directory.Build.props`?**

The `Directory.Build.props` file is an MSBuild configuration file that allows you to define properties and targets that are shared by all projects within a directory and its subdirectories. It's a way to centralize common build settings, reducing redundancy and ensuring consistency across your projects.

**Why use `Directory.Build.props`?**

Without `Directory.Build.props`, you would likely have duplicate build settings in each project's `.csproj` file. This can lead to:

- **Redundancy:** Repeating the same settings multiple times.
- **Inconsistency:** Projects might have different settings, leading to unexpected behavior.
- **Maintenance Overhead:** Changing a setting requires modifying multiple `.csproj` files.

`Directory.Build.props` solves these problems by:

- **Centralizing Build Settings:** Define common properties and targets in one place.
- **Ensuring Consistency:** All projects inherit these settings.
- **Simplifying Maintenance:** Update a setting in one place, and the change applies to all projects.

**Customization:**

The `Directory.Build.props` file is highly customizable. This guide covers the most common scenarios, but you can explore advanced features in the official Microsoft documentation: [Customize by directory](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory?view=vs-2022).

**Example `Directory.Build.props` Structure:**

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <NoWarn>CS1591;CS1573;RCS1079;IDE0022</NoWarn>
    <AccelerateBuildsInVisualStudio>true</AccelerateBuildsInVisualStudio>
    <PublishRootDir>$(MSBuildThisFileDirectory)\out</PublishRootDir>
  </PropertyGroup>

  <Target Name="CleanPublishAssets" BeforeTargets="Build">
    <RemoveDir Directories="$(PublishDir)" />
  </Target>

  <Target Name="MovePublishedAssets" AfterTargets="Publish">
    <ItemGroup>
      <PublishedAssets Include="$(OutDir)\publish\**\*.*" />
    </ItemGroup>
    <Copy SourceFiles="@(PublishedAssets)" DestinationFolder="$(PublishRootDir)" SkipUnchangedFiles="true" />
  </Target>
</Project>
```

**Explanation of Settings:**

- **`<PropertyGroup>`:** Contains common build properties.
  - **`<TargetFramework>`:** The target framework for all projects (e.g., `net8.0`).
  - **`<ImplicitUsings>`:** Whether to enable implicit using directives (typically `disable` for better control).
  - **`<Nullable>`:** Whether to enable nullable reference types (typically `disable` to opt-out).
  - **`<TreatWarningsAsErrors>`:** Whether to treat warnings as errors (highly recommended for code quality).
  - **`<NoWarn>`:** A list of warning codes to suppress.
  - **`<AccelerateBuildsInVisualStudio>`:** A flag to improve build performance in Visual Studio.
  - **`<PublishRootDir>`:** The root directory for published output. `$(MSBuildThisFileDirectory)` refers to the directory containing the `Directory.Build.props` file.
- **`<Target>`:** Defines custom build targets.
  - **`CleanPublishAssets`:** A target that runs _before_ the `Build` target to clean the publish directory.
  - **`MovePublishedAssets`:** A target that runs _after_ the `Publish` target to copy the published assets to the `PublishRootDir`. This uses Item Groups (`<ItemGroup>`) and the `Copy` task for efficient copying.

**How it works in practice:**

1.  Place a `Directory.Build.props` file in the root directory of your solution (or in a subdirectory to apply settings to a subset of projects).
2.  Define the common properties and targets in this file.
3.  All projects in the directory (and its subdirectories) will automatically inherit these settings.
4.  You can still override these settings in individual project files if needed.

## Again: There are more to customize for this file, but for me, this is enough.
