###################
# Build stage
###################

# Pull dotnet sdk
FROM bitnami/dotnet-sdk:8.0.404-debian-12-r3 AS build

# Set working directory
WORKDIR /build

# Copy in essential files
COPY ./SetupProject.sln .
COPY ./global.json .
COPY ./nuget.config .
COPY ./Directory.Packages.props .
COPY ./Directory.Build.props .
COPY ./.editorconfig .

# Try to restore the project
RUN dotnet restore

# Copy the rest
COPY . .

# Try to build the project
RUN dotnet build --no-restore -c Release

# Try to publish the project into publish folder
RUN dotnet publish --no-restore --no-build -c Release --property:PublishDir=/build/publish

###################
# Final stage
###################

# Pull aspnet core runtime
FROM bitnami/aspnet-core:8.0.11-debian-12-r5

# Set working directory
WORKDIR /app

# Copy publish folder from build stage
COPY --from=build /build/publish ./
COPY api_local_https.pfx ./

# Run the application.
ENTRYPOINT ["dotnet", "EntryPoint"]