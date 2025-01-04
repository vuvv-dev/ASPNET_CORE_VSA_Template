# This is publish script for the project (Window version)
# Run this file for publishing and check the out assets 
# In out folder (situated in SETUPPROJECT/out)

# Set error handling
$ErrorActionPreference = "Stop"

# Format the project
Write-Output "Publish project..."
dotnet publish -c Release --no-build --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed"
    exit $LASTEXITCODE
}
