# ASPNET_CORE_VSA_Template

=== Vertical Slice Architecture Template ===

## TL;DR

- WindowsThe ASP.NET Core template follows the Vertical Slice Architecture (VSA)..

- Detail of each config file: [Coming soon]()

- For how to test the project, please check out this [Getting Started](#getting-started)

- For project, I use [Fast Endpoint](https://fast-endpoints.com/) to construct api because while it is easy to write, it is WAY MORE READABLE due to fluent API, but you can use controller or minimal api if you want.

## Introduction 😊😊

Welcome to the ASP NET Core Vertical Slice Architecture Template! This template is a starting point for building an ASP NET Core project following the Vertical Slice Architecture (VSA) while maintaining source code readable when the source gets larger.

## Reason

In the past, I often followed tutorials to build projects, but I encountered significant challenges with the project structures they suggested. Here are some common issues I faced:

- **Long service files**: These became difficult to maintain as they grew in size.
- **Scattered context**: Retrieving all the information related to a feature was challenging because methods and classes were spread across multiple files and folders.
- **Fat controllers**: Controllers often became bloated and hard to manage. 🤦‍♂️

I realized that this approach, known as **technical-based architecture**, groups all files of the same type (e.g., services in a `Services` folder). While this approach seemed organized, it often led to inefficiencies. To address this, I researched **feature-based architecture**, which has the following advantages:

- Each feature has its own dedicated folder, making the structure easier to navigate.
- All files related to a feature are stored together, simplifying maintenance. 😍😍

However, even with a feature-based architecture, I encountered new challenges as the project grew:

- **Class naming conflicts**: With multiple feature-related classes in one project, it was easy to mistype or confuse class names (e.g., `FM1Endpoint` vs. `FM2Endpoint`). ☹️

To solve this, I decided to move each feature into its own class library. This approach makes it impossible to call classes from one feature without explicitly referencing its assembly, reducing the chances of errors.

And that’s how this template was born! 🤣🤣

## Demo video

[Coming soon]()

## Features

- Demo web api with 1 feat (will upgrade for more).

## Getting Started

### Prerequisites

Ensure you have the following installed:

- DOTNET SDK [8.0.404](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Installation

Clone this project via this command:

```bash
git clone https://github.com/Jackpieking/VerticleSliceArchitectureTemplate.git
```

## Usage

### 1. Navigate to the root of the template

- Below here is some example of cd to the template folder, just change the dir to meet your need, as long as you get into the root of template folder that you have just cloned

#### Example:

- Windows

```bash
cd E:\CODE_PROJECTS\VerticleSliceArchitectureTemplate\
```

- Mac/Linux

```bash
cd E:/CODE_PROJECTS/VerticleSliceArchitectureTemplate\
```

### 2. Install necessary dotnet tool via this command:

- Windows

```bash
.\Scripts\Init\init.ps1
```

- Mac/Linux

```bash
./Scripts/Init/init.sh
```

### 3. Build the project via this command:

- Windows

```bash
.\Scripts\Build\build.ps1
```

- Mac/Linux

```bash
./Scripts/Build/build.sh
```

### 3. Run the project via this command:

- Windows

```bash
Coming soon
```

- Mac/Linux

```bash
Coming soon
```

## License

VSA_Template is licensed under the Apache V2.0 License. See [LICENSE](https://github.com/Jackpieking/VerticleSliceArchitectureTemplate/blob/master/LICENSE) for more details.

## Contact

For inquiries, please submit issues on [GitHub](https://github.com/Jackpieking/VerticleSliceArchitectureTemplate).

---

Happy coding with this project! 😁
