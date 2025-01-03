# This is build script for the project (Window version)
# Execute this to build the project

# Set error handling
$ErrorActionPreference = "Stop"

# Constants
$CONFIGURATION_MODE = 'Release'
$PROJECT_NAME = 'SetupProject'
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

Write-Output "Format project (csharpier)..."
dotnet csharpier .
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet format failed"
    exit $LASTEXITCODE
}

Write-Output "Restore project..."
dotnet restore $projectRoot\SetupProject.sln
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet restore failed"
    exit $LASTEXITCODE
}

Write-Output "Build project..."
dotnet build --no-restore -c $CONFIGURATION_MODE $projectRoot\SetupProject.sln
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet build failed"
    exit $LASTEXITCODE
}

# Set back to original directory
Set-Location $CURRENT_PATH