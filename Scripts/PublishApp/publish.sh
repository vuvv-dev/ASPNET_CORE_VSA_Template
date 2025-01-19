#!/bin/bash

# This is publish script for the project (Linux/Mac version)
# Run this file for publishing and check the out assets 
# In out folder (situated in SETUPPROJECT/out)

# Change this to your project name
PROJECT_NAME="ASPNET_CORE_VSA_Template"

# Exit on errors
set -e

# Constants
CURRENT_PATH=$(pwd)

# Function to find the root directory containing the solution file
find_project_root() {
    current_dir=$1
    while true; do
        # Try to find the .sln file in the current directory
        sln_file=$(find "$current_dir" -maxdepth 1 -name "*.sln" -print -quit)
        if [ -n "$sln_file" ]; then
            echo "$current_dir"
            return
        fi

        # Try to go up one directory level
        parent_dir=$(dirname "$current_dir")
        if [[ "$parent_dir" != *"$PROJECT_NAME"* ]]; then
            return
        fi
        current_dir="$parent_dir"
    done
}

# Determine the project root path containing the solution file
project_root=$(find_project_root "$(dirname "$(realpath "$0")")")
if [ -z "$project_root" ]; then
    echo "No solution file (.sln) found in the directory hierarchy."
    exit 1
fi

# Set to working directory to project root
echo "Project root path determined: $project_root"
cd "$project_root"

# Format the project
echo "Publish project..."
if ! dotnet publish -c Release --no-build --no-restore; then
    echo "dotnet publish failed"
    exit 1
fi

# Go back to original directory
cd "$CURRENT_PATH"