# This is build script for the project (Window version)
# Execute this to build the project

# Set error handling
$ErrorActionPreference = "Stop"

# Constants
$CONFIGURATION_MODE = 'Release'
$CURRENT_PATH = Get-Location

# Project name key and value
$PROJECT_NAME_KEY = 'PROJECT_NAME'
$PROJECT_NAME_VALUE = ''

# Solution file name key and value
$SLN_FILE_NAME_KEY = 'SLN_FILE_NAME'
$SLN_FILE_NAME_VALUE = ''

# Define the path to the .env file
$parentPath = Split-Path -Path $PSScriptRoot -Parent
$envFilePath = "$parentPath\.env"

# Read the .env file and parse key-value pairs
if (Test-Path $envFilePath) {
    $envVars = @{}

    Get-Content $envFilePath | ForEach-Object {
        if ($_ -match "^\s*([^#][\w]+)\s*=\s*(.+)\s*$") {
            $envVars[$matches[1]] = $matches[2]
        }
    }

    # Check and assign values explicitly
    if ($envVars.ContainsKey($PROJECT_NAME_KEY)) {
        $PROJECT_NAME_VALUE = $envVars[$PROJECT_NAME_KEY]
    }

    if ($envVars.ContainsKey($SLN_FILE_NAME_KEY)) {
        $SLN_FILE_NAME_VALUE = $envVars[$SLN_FILE_NAME_KEY]
    }
}

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
        if ($parentDir -contains $PROJECT_NAME_VALUE) {
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

Write-Output "Build project..."
dotnet build -c $CONFIGURATION_MODE .\$SLN_FILE_NAME_VALUE
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet build failed"
    exit $LASTEXITCODE
}

# Set back to original directory
Set-Location $CURRENT_PATH