# This is test executable file for the project (Window version)
# Run this file for testing and check the generated html report 
# In test result folder (situated in SETUPPROJECT/TestResults)

# Change this to your project name
$PROJECT_NAME = 'ASPNET_CORE_VSA_Template'

# Set error handling
$ErrorActionPreference = "Stop"

# Constants
$CURRENT_PATH = Get-Location

# Function to find the root directory containing the solution file
function Find-ProjectRoot {
    param (
        [string] $currentDir
    )

    while ($true) {
        # Try to find the sln in the current directory, if found, return something
        # otherwise, nothing return
        #
        # First 1 is from the first of the list, take 1 element
        $slnFile = Get-ChildItem -Path $currentDir -Filter *.sln -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($slnFile) {
            return $currentDir
        }

        # Try to back 1 level directory since, the sln is 
        # always in the root directory, so must go back
        # not going forward
        #
        # Then we assign new path to current dir
        $parentDir = Split-Path -Path $currentDir -Parent
        if ($parentDir -contains $PROJECT_NAME) {
            return $null
        }
        $currentDir = $parentDir
    }

    return $null
}

# Determine the project root path containing the solution file
$projectRoot = Find-ProjectRoot -currentDir $(Split-Path -Path $MyInvocation.MyCommand.Path -Parent)
if (-not $projectRoot) {
    Write-Error "No solution file (.sln) found in the directory hierarchy."
    exit 1
}

# Set to working directory to project root
Write-Output "Project root path determined: $projectRoot"
Set-Location $projectRoot

# Remove the old test results
Write-Output "Remove old test results..."
Remove-Item ".\TestResults" -Recurse -Force -ErrorAction SilentlyContinue

# Run the tests
Write-Output "Run tests..."
dotnet test --logger "console" --blame --collect "XPlat Code coverage" --results-directory ".\TestResults\"
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet test failed"
    exit $LASTEXITCODE
}

# Generate the report
Write-Output "Generate report..."
dotnet reportgenerator "-reports:.\TestResults\*\coverage.cobertura.xml" "-targetdir:.\TestResults\coverage" "-reporttypes:HtmlInline_AzurePipelines;Cobertura"

# Set back to original directory
Set-Location $CURRENT_PATH