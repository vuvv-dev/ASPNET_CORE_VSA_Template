#!/bin/bash

# This is init script for the project (Linux/Mac version)
# Execute this to init the neccessary tools for the project

# Set error handling
set -e

# Format the project
echo "Install necessary dotnet tools..."
if ! dotnet tool restore; then
    echo "dotnet tool restore failed"
    exit 1
fi