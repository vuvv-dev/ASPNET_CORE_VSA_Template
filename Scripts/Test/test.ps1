# This is test executable file for the project (Window version)
# Run this file for testing and check the generated html report 
# In test result folder (situated in SETUPPROJECT/TestResults)

# Set error handling
$ErrorActionPreference = "Stop"

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