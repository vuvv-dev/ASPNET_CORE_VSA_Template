# 📦 Directory.Packages.props

| ⚡ TL;DR (quick version)                                                                                                                                                                                                                                                  |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| The `Directory.Packages.props` file centralizes **package version management** for .NET projects in a solution. It ensures consistent package versions across multiple projects, simplifies updates, and reduces redundancy by defining shared dependencies in one place. |

The `Directory.Packages.props` file is a configuration file used in .NET solutions to centralize package version management. It allows you to define common package versions in a single file, ensuring consistency across all projects in a solution.

This is especially useful in large solutions with multiple projects, where managing package versions separately can lead to conflicts or inconsistencies.

### How does it work

Without `Directory.Packages.props`:

- Each project defines its own package versions in its `.csproj` file.

- Updating a package requires modifying multiple `.csproj` files.

- Version mismatches between projects can cause build or runtime issues.

With `Directory.Packages.props`:

- Common package versions are defined once in a central location.

- All projects in the solution automatically inherit these versions.

- Updating a package requires changing the version in just one file.

### Can you customize the file

There are more to customize for this file, you can checkout [Directory.Packages.props](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) in microsoft documents to customize for more.

### Structure

Here’s the structure of the [Directory.Packages.props](/Directory.Packages.props) file in this project:

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>

  <!-- Dependencies center -->
  <ItemGroup>
    <PackageVersion Include="FastEndpoints" Version="5.33.0" />
  </ItemGroup>
</Project>
```

### Explanation of Settings

#### 1. Centralized Version Management

- The `<PackageVersion>` elements define the package names and versions to be shared across projects.

- Ensures all projects use the same version of a package.

#### 2. Enable Central Management

- `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>` activates centralized package version management for the solution.

#### 3. Simplified Maintenance

- Updates are made in one file, automatically propagating to all dependent projects.

## Again: There are more to customize for this file, but for me, this is enough, you can checkout [Directory.Packages.props](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) in microsoft documents to customize for more.
