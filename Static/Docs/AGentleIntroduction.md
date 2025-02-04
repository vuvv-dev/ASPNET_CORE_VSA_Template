# 🦄 A Gentle Introduction

In my view, an application typically contains two types of features:

- **Non-Functional Features**: These features focus on aspects like performance, security, or usability. Examples include caching or logging. While they don't directly affect the core functionality of the app, they are crucial for the system's overall health.

- **Functional Features**: These represent the main functionalities of the system, such as user logins, report generation, or data processing. These features directly impact how users interact with the application.

Each feature usually consists of 1 to 3 components or maybe more if needed, depending on the complexity:

- **Presentation (Optional)**: This is the entry point for user interaction with the feature, such as API endpoints or views. Some features may not need a presentation layer (e.g., background tasks like cleaning or scheduled processes).

- **Business Logic (Required)**: This is the core logic of the feature. Every feature must have business logic to function.

- **Data Access (Optional)**: This component allows the feature to interact with databases or storage systems to retrieve or store data. Not all features require data access—some may operate independently, like background processes that don't interact with a database.

In larger projects, you may have hundreds of features. VSA stands out as an excellent choice because it organizes the codebase into **self-contained and independent feature modules**, which makes it easier to maintain and scale as the project grows.

However, **VSA is just an approach** to structure your code. It's up to you to implement it in a way that works best for your project.

==> This template represents my own approach to implementing VSA.

Below, you'll find a brief description of each file and folder in this project.

## 📂 Src

Place for all source code that is used in this project.

## 🛠️ nuget.config ([more](NugetConfig.md))

It is used to configure how NuGet handles package sources and settings for your project. It allows you to:

- Define specific repositories (sources) from which NuGet can download packages.

- Ensure all team members use the same package sources to maintain consistency.

- Customize package restore behavior when running commands like dotnet restore.

By using this file, you can centralize and standardize NuGet settings, making it easier to manage dependencies across your project.

Read more [**here**](NugetConfig.md).

## 🌍 global.json ([more](GlobalJson.md))

It is used to specify the .NET SDK version your project requires. By defining a specific SDK version, you ensure that everyone working on the project, as well as all stages like development, testing, and production, use the same version. This helps maintain a stable and consistent environment throughout the project's lifecycle.

Read more [**here**](GlobalJson.md).

## 🗂️ SetupProject.sln

It is a solution file that organizes and groups multiple related .NET projects (such as webapi, classlib, etc.) into a single workspace. This file is essential for managing the structure and dependencies of a multi-project solution in .NET.

## 📦 Directory.Packages.props ([more](DirectoryPackagesProps.md))

It is used to centralize and manage NuGet package references for the entire project. By consolidating package definitions in this file:

- All modules in the project can reference dependencies from a single location.

- Ensures consistent package versions across all modules, preventing version mismatches.

- Simplifies dependency management, making it easier to update or modify package versions globally.

This approach benefits the entire project by promoting stability and reducing the risk of compatibility issues between modules.

Read more [**here**](DirectoryPackagesProps.md).

## 📂 Directory.Build.props ([more](DirectoryBuildProps.md))

It file is used to define common MSBuild properties and configurations that apply to all projects in the directory and its subdirectories. By using this file, you will have following advantages:.

- Centralized Configuration: Common settings (e.g., TargetFramework, OutputPath) are managed in one place, ensuring consistency across projects.

- Simplified Maintenance: Instead of repeating the same configurations in every project file, you define them once here.

- Overrides: Individual project files can override the settings if needed, offering flexibility.
  By using Directory.Build.props, you make the project setup cleaner, easier to maintain, and more scalable as the solution grows.

Read more [**here**](DirectoryBuildProps.md).

## 🔴 .gitignore

Every project should have a `.gitignore` file to prevent unnecessary files from being committed to git.

## 🔴 .gitattributes

Just use it for defining behavior for file in git, like how line end should be.

## 🔧 .csharpierrc.json

It is used to configure the behavior of the C# format tool (CSharpier), like how many tabs, how many spaces, etc. Just like how prettier works.

## 📂 Scripts ([more](CustomizeScript.md))

Place for all scripts that is used in this project such as build, publish, etc.

Read more [**here**](CustomizeScript.md).

## ⚙️ .github/workflows

Place for workflows that run in github, maybe after commit or pull request or many other scenarios.

## ⚙️ .config/dotnet-tools.json ([more](DotnetToolConfig.md))

This file contains metadata about the dotnet tool that the project use like csharpier, dotnet-ef,...

Read more [**here**](DotnetToolConfig.md).

## ⚙️ app-assembly.json

- Place for assembly that have its own services, and these assemblies require these services to be registered.

- The entry point can scan the list of assembly names in this file and check for services that need to registered of each module and add them to the service collection.

- Basically, you want to leave this file alone, unless you introduces new c# module like classlib, web api or other project and these module may have services that need to be **REGISTERED**, you may want to modify this file to include the name of new module.
