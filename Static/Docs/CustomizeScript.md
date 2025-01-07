# 📂 Customize Script

| ⚡ TL;DR (quick version)                                                                            |
| --------------------------------------------------------------------------------------------------- |
| Change `PROJECT_NAME` value at the top of some scripts to make them work with different folder name |

When cloning this project, you may need to use a folder name different from the default one. This could happen due to project constraints, customer requirements, or other reasons. To make the project scripts work with your custom folder name, you'll need to update a specific variable in the scripts.

### How to customize

The following `SCRIPTS` require changes:

- `./Scripts/Build/build.sh` or `./Scripts/Build/build.ps1`

- `./Scripts/Clean/clean.sh` or `./Scripts/Clean/clean.ps1`

- `./Scripts/Run/run.sh` or `./Scripts/Run/run.ps1`

You need to find and update the `PROJECT_NAME` variable. Below is the place of the variable in each script:

#### 1. PowerShell Scripts (`.ps1`):

At the top of each `.ps1` script, you will find a line like this:

```ps1
# Change this to your project name
$PROJECT_NAME="ASPNET_CORE_VSA_Template"
```

Update the value of `$PROJECT_NAME` to match the name of the folder where you cloned the repository.

#### 2. Shell Scripts (`.sh`):

At the top of each `.sh` script, you will find a line like this:

```sh
# Change this to your project name
PROJECT_NAME="ASPNET_CORE_VSA_Template"
```

Update the value of `PROJECT_NAME` to match the name of the folder where you cloned the repository.

### Example Update

If you cloned the repository into a folder named `First_Project`, update the scripts as follows:

- #### PowerShell Scripts (`.ps1`):

```ps1
# Change this to your project name
$PROJECT_NAME="First_Project"
```

- #### Shell Scripts (`.sh`):

```sh
# Change this to your project name
PROJECT_NAME="First_Project"
```

## There are more to customize for this file, but for me, this is enough.
