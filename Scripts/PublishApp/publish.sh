#!/bin/bash

# This is publish script for the project (Linux/Mac version)
# Run this file for publishing and check the out assets 
# In out folder (situated in SETUPPROJECT/out)

# Set error handling
set -e

# Format the project
echo "Publish project..."
if ! dotnet publish -c Release --no-build --no-restore; then
    echo "dotnet publish failed"
    exit 1
fi