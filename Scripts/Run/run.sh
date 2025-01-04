#!/bin/bash

# This is build script for the project (Linux/Mac version)
# Execute this to build the project

# Exit on errors
set -e

# Constants
CONFIGURATION_MODE="Release"
PROJECT_NAME="ASPNET_CORE_VSA_Template"

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

echo "Run project..."
dotnet run -c Release --project "$project_root/Src/Entry/Entry.Src/"
if [ $? -ne 0 ]; then
    echo "dotnet run failed"
    exit 1
fi
