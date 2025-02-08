## ⚙️ .config/dotnet-tools.json ([more](DotnetToolConfig.md))

This file lists the .NET tools your project uses, like CSharpier (for code formatting) and dotnet-ef (for Entity Framework). Think of it as a list of extra helpers your project relies on.

Read more [**here**](DotnetToolConfig.md).

## ⚙️ .github/workflows

This folder contains files that define automated workflows on GitHub. These workflows can be triggered by events like commits, pull requests, or other actions. They automate tasks like building, testing, and deploying your code.

## 📂 AppInfrastructure

This folder contains Dockerfiles and configuration related to the main application container itself

If you want to customize anything in this folder, you should have some knowledge about docker first (e.g., docker compose, dockerfile, etc.)

If you want to run, please check out how to run it here [**here**](RunDockerStack.md)

## 📂 Scripts ([more](CustomizeScript.md))

This folder holds scripts used for various project tasks, such as building the project, publishing it, or running other custom operations.

Read more [**here**](CustomizeScript.md).

## 📂 Src

This is where all your project's source code lives. It's the heart of your application.

## 🔧 .csharpierrc.json

This file configures the CSharpier code formatting tool. It defines rules for things like indentation (tabs vs. spaces), line length, and other stylistic choices. It keeps your code looking consistent. Just like how prettier works.

## 🔴 .gitattributes

This file tells Git how to handle specific files, especially regarding line endings (Windows vs. Linux style). It helps ensure consistency across different operating systems.

## 🔴 .gitignore

Crucially, this file specifies files and folders that Git should ignore and not track in version control. This prevents unnecessary files (like build outputs or temporary files) from cluttering your repository. Every project should have one.

## 📂 Directory.Build.props ([more](DirectoryBuildProps.md))

It file is used to define common MSBuild properties and configurations that apply to all projects in the directory and its subdirectories. By using this file, you will have following advantages:.

- Centralized Configuration: Common settings (e.g., TargetFramework, OutputPath) are managed in one place, ensuring consistency across projects.

- Simplified Maintenance: Instead of repeating the same configurations in every project file, you define them once here.

- Overrides: Individual project files can override the settings if needed, offering flexibility.
  By using Directory.Build.props, you make the project setup cleaner, easier to maintain, and more scalable as the solution grows.

Read more [**here**](DirectoryBuildProps.md).

## 📦 Directory.Packages.props ([more](DirectoryPackagesProps.md))

It is used to centralize and manage NuGet package references for the entire project. By consolidating package definitions in this file:

- All modules in the project can reference dependencies from a single location.

- Ensures consistent package versions across all modules, preventing version mismatches.

- Simplifies dependency management, making it easier to update or modify package versions globally.

This approach benefits the entire project by promoting stability and reducing the risk of compatibility issues between modules.

Read more [**here**](DirectoryPackagesProps.md).

## 🗂️ SetupProject.sln

This is the solution file. It organizes all the different projects (like your web API, class libraries, etc.) that make up your application. It's the main file you open in `Visual Studio` or other IDE like `Rider`

## 🔧 app-assembly.json

This file lists all the assemblies (collections of code) that contain services your application needs. It's like a directory telling the application where to find these services.

**How do assemblies register their services?**

Each assembly _should_ have a `RegistrationCenter` file (which is a class) that knows how to register the services within that assembly. This `RegistrationCenter` file is crucial for the application to understand what services are available.

Below is a simple example of working a module:

Let's say you're adding a new feature called "F100". Here's the process:

1. **Create the assembly:** You create a new assembly (a class library project) named `F100`. It usually lives in a specific folder, like `Src/Core/F100`.

2. **Create the RegistrationCenter:** Inside the `F100` assembly, you create a `RegistrationCenter` file (a class). This file is responsible for registering the services that belong to the `F100` feature. It _must_ inherit from `IServiceRegister`.

3. **Update `app-assembly.json`:** Open the `app-assembly.json` file. Add the _name_ of your new assembly (`F100` in this example) to the appropriate section (e.g., the `Core` section if it's a core feature).

**That's it!** By adding the assembly name to `app-assembly.json`, you've told the application to look inside that assembly for its `RegistrationCenter` and register the services it contains.

**Why _should_ every assembly have a `RegistrationCenter`, even if it doesn't have services yet?**

Even if your new assembly doesn't have any services _right now_, it's best practice to create the `RegistrationCenter` file anyway. You might need to add services to it later. Having the `RegistrationCenter` in place from the start prevents you from having to refactor and add it later, making future development smoother. It's like planning ahead!

## 🌍 global.json ([more](GlobalJson.md))

This file specifies the .NET SDK version your project requires. This ensures everyone working on the project uses the same SDK, preventing compatibility issues.

Read more [**here**](GlobalJson.md).

## 🛠️ nuget.config ([more](NugetConfig.md))

This file configures how NuGet (the package manager) works. It lets you define where NuGet should look for packages and customize its behavior. It helps ensure consistent package management across the team.

Read more [**here**](NugetConfig.md).
