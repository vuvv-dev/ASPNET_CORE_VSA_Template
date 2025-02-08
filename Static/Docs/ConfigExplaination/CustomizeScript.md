# Customizing Project Scripts for Different Folder Names

This guide explains how to modify the project scripts to work with custom folder names when cloning the repository.

**Why Customize?**

When cloning the repository, you might want to use a folder name other than the default (`ASPNET_CORE_VSA_Template`). The project scripts rely on this default name, so you'll need to update them to reflect your custom folder name.

**Which Scripts to Update?**

The following scripts need modification:

- `./Scripts/Build/build.sh` or `./Scripts/Build/build.ps1` (Build scripts)

- `./Scripts/Clean/clean.sh` or `./Scripts/Clean/clean.ps1` (Clean scripts)

- `./Scripts/Run/run.sh` or `./Scripts/Run/run.ps1` (Run scripts)

- `./Scripts/PublishApp/publish.sh` or `./Scripts/PublishApp/publish.ps1` (Publish scripts)

- `./Scripts/Test/test.sh` or `./Scripts/Test/test.ps1` (Test scripts)

**How to Update the Scripts:**

In each of the scripts listed above, you need to change the value of the `PROJECT_NAME` variable.

**1. PowerShell Scripts (`.ps1`):**

At the beginning of each `.ps1` script, you'll find a line like this:

```powershell
# Change this to your project name
$PROJECT_NAME = "ASPNET_CORE_VSA_Template"
```

Change the value within the double quotes (`"ASPNET_CORE_VSA_Template"`) to the actual name of the folder where you cloned the repository.

**2. Shell Scripts (`.sh`):**

At the beginning of each `.sh` script, you'll find a line like this:

```bash
# Change this to your project name
PROJECT_NAME="ASPNET_CORE_VSA_Template"
```

Change the value within the double quotes (`"ASPNET_CORE_VSA_Template"`) to the actual name of the folder where you cloned the repository.

**Example:**

Let's say you cloned the repository into a folder named `MyProject`. You would update the scripts as follows:

**PowerShell (`.ps1`):**

```powershell
# Change this to your project name
$PROJECT_NAME = "MyProject"
```

**Shell (`.sh`):**

```bash
# Change this to your project name
PROJECT_NAME="MyProject"
```

## Again: There are more to customize for this file, but for me, this is enough.
