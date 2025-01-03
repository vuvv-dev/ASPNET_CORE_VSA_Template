#!/bin/bash

# This is a test executable file for the project (Linux/Mac version)
# Run this file for testing and check the generated HTML report 
# In test result folder (situated in SETUPPROJECT/TestResults)

# Set error handling
set -e

# Remove the old test results
echo "Remove old test results..."
rm -rf "./TestResults"

# Run the tests
echo "Run tests..."
if ! dotnet test --logger "console" --blame --collect "XPlat Code coverage" --results-directory "./TestResults/"; then
    echo "dotnet test failed"
    exit 1
fi

# Generate the report
echo "Generate report..."
dotnet reportgenerator "-reports:./TestResults/*/coverage.cobertura.xml" "-targetdir:./TestResults/coverage" "-reporttypes:HtmlInline_AzurePipelines;Cobertura"
