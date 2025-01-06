# 📦 Directory.Build.props

| ⚡ TL;DR (quick version)                                                                                                                                                                                                                                                                                      |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| The `Directory.Build.props` file centralizes shared MSBuild properties and configurations for all projects in a directory and its subdirectories. It simplifies project setup, ensures consistency, and reduces duplication by defining common build settings, dependencies, and configurations in one place. |

The `Directory.Build.props` file is an MSBuild configuration file that applies shared settings and custom build logic to all projects in the directory and its subdirectories. The file reduces redundancy by centralizing common properties and build tasks, ensuring consistency across multiple projects.

### How does it work

Without `Directory.Build.props`:

- Duplicate settings and configurations like `TargetFramework` or `Nullable` in project's `.csproj` file.

With `Directory.Build.props`:

- All common settings (e.g., `TargetFramework`, `Nullable`) are defined once.

- All projects in the solution automatically inherit these settings.

- Still allow to customize some settings in individual projects.

### Can you customize the file

There are more to customize for this file, you can checkout [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory?view=vs-2022) in microsoft documents to customize for more.

### Structure

Here’s the structure of the [Directory.Build.props](/Directory.Build.props) file in this project:

```xml
<Project>
    <!-- General config -->
    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>disable</ImplicitUsings>
        <Nullable>disable</Nullable>
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
        <NoWarn>CS1591;CS1573;RCS1079;IDE0022</NoWarn>
        <AccelerateBuildsInVisualStudio>true</AccelerateBuildsInVisualStudio>
    </PropertyGroup>

    <!-- Set the publish directory -->
    <PropertyGroup>
        <PublishRootDir>$(MSBuildThisFileDirectory)\out</PublishRootDir>
    </PropertyGroup>

    <!-- Clean the publish assets after build -->
    <Target Name="CleanPublishAssets" BeforeTargets="Build">
        <RemoveDir Directories="$(PublishDir)"></RemoveDir>
    </Target>

    <!-- Move the published assets to the publish directory -->
    <Target Name="MovePublishedAssets" AfterTargets="Publish">
        <ItemGroup>
            <!-- Pattern for getting publish folder of any projects -->
            <PublishedAssets Include="$(OutDir)\publish\**\*.*" />
        </ItemGroup>

        <!--
            Iterate through each publish folder and
            copy the files to the publish directory
        -->
        <Copy SourceFiles="@(PublishedAssets)"
            DestinationFolder="$(PublishRootDir)"
            SkipUnchangedFiles="true" />
    </Target>
</Project>
```

### Explanation of Settings

#### 1. General Project Configuration

- **Target Framework**: Sets the target framework to `.NET 8.0`.

- **Implicit Usings**: Disables implicit using directives for better control over namespace imports.

- **Nullable Context**: Disables nullable reference type checks.

- **Treat Warnings as Errors**: Ensures all warnings are treated as errors, improving code quality.

- **Ignored Warnings**: Suppresses specific warnings (e.g., `CS1591`, `CS1573`).

- **Build Acceleration**: Improves build performance in Visual Studio by enabling `AccelerateBuildsInVisualStudio`.

#### 2. Publish Directory Configuration

- **PublishRootDir**: Defines a custom root directory for published assets (`out`). This keeps the output organized and separate from other build artifacts.

#### 3. Custom Build Tasks

- **Clean Publish Assets**:

  - Before each build, the `CleanPublishAssets` target removes the contents of the publish directory to ensure a clean build output.

- **Move Published Assets**:

  - After publishing, the `MovePublishedAssets` target gathers all published files and moves them to the defined `PublishRootDir`.

  - Uses a wildcard pattern (\*_\*._) to include all published assets and efficiently copies them while skipping unchanged files.

## Again: There are more to customize for this file, but for me, this is enough, you can checkout [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory?view=vs-2022) in microsoft documents to customize for more.
