# Part 4 - Workflow Explanation

## Recall

In [Part 3](./Part3.md), we explored the code structure and its components, including features and sub-features. Now, let's go through some example **workflows**

### Introduce A New Feature Workflow (e.g., `F22`)

#### 1. Create a New Feature/Sub-Feature Assembly (Class Library)

To streamline this process, a local template CLI has been set up.

- Navigate to `./Templates/Feature` and install the template by running:

  ```bash
  dotnet new install .
  ```

  This will install a new template called `tcfeat`, which you can use to create feature assemblies.

  ![Install Template](../../Images/Install-New-Template-Cli.png)

#### 2. Generate the New Feature Module

- Navigate back to the root directory and go to `./Src/Core`.
- Run the following command to create a new feature module named `F22`:

  ```bash
  dotnet new tcfeat -o F22
  ```

  This will generate a new feature module inside `./Src/Core/F22`.

  ![Create F22 Feat Module](../../Images/Create-F22-Feat-Module.png)

  ![New F22 Feat Module](../../Images/New-F22.png)

#### 3. Add the New Module to the Solution File

- Navigate back to the root directory and add the new feature module to the `.sln` file:

  ```bash
  dotnet sln .\Project.sln add .\Src\Core\F22\F22.csproj
  ```

  This ensures that `F22` is included in the solution.

  ![Add F22 To Sln](../../Images/Add-F22-To-Sln.png)

  ![F22 In Sln](../../Images/New-F22-Module-In-Sln.png)

#### 4. Register the New Module

- Open `app-assembly.json` in the root directory and add the `F22` module name.

  ![Add F22 Name To App Assembly](../../Images/Add-F22-Name-To-App-Assembly-File.png)

#### 5. Build the Project

To verify that everything is correctly set up, rebuild the project:

- On Windows:

  ```bash
  .\Scripts\Build\build.ps1
  ```

- On Linux/Mac:

  ```bash
  ./Scripts/Build/build.sh
  ```

  ![Build Project](../../Images/Build-Project.png)

If the build succeeds, **congratulations!** You are now ready to start coding the `F22` module.
