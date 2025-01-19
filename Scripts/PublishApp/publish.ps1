# This is publish script for the project (Window version)
# Run this file for publishing and check the out assets 
# In out folder (situated in SETUPPROJECT/out)

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

# Format the project
Write-Output "Publish project..."
dotnet publish -c Release --no-build --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed"
    exit $LASTEXITCODE
}

# Set back to original directory
Set-Location $CURRENT_PATH
