# Build and Deployment Guide

This document describes how to build the Bento .NET SDK and deploy releases.

## Prerequisites

- .NET 8.0 SDK or later
- Git
- GitHub account with appropriate permissions

## Local Development

### Building the Project

1. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

2. **Build the solution:**
   ```bash
   dotnet build --configuration Release
   ```

3. **Build specific project with output:**
   ```bash
   # Standard build to bin directory
   dotnet build Bento/Bento.csproj --configuration Release
   
   # Copy artifacts to custom directory if needed
   mkdir -p ./artifacts
   cp Bento/bin/Release/net8.0/* ./artifacts/
   ```

### Running Tests

Tests will run automatically if test projects are detected:
```bash
dotnet test --configuration Release --verbosity normal
```

### Creating NuGet Package

```bash
# Build first, then pack
dotnet build Bento/Bento.csproj --configuration Release
dotnet pack Bento/Bento.csproj --configuration Release --output ./packages --no-build
```

## Automated CI/CD

The project uses GitHub Actions for automated builds and deployments:

### Build and Publish (`build-and-publish.yml`)

**Triggers:**
- Push to `main` or `develop` branches
- Pull requests to `main` branch  
- Push of version tags (e.g., `v1.2.3`)

**What it does:**
- Builds the solution
- Runs tests (if available)
- Creates DLL artifacts for regular builds
- **On tag push**: Replaces version `1.0.0` with tag version, creates release with artifacts

## Release Process

### 1. Prepare for Release

1. **Ensure all changes are committed and pushed:**
   ```bash
   git add .
   git commit -m "Prepare for release"
   git push origin main
   ```

2. **Note**: The version in `Bento.csproj` should remain `1.0.0` - it will be automatically replaced during release.

### 2. Create a Release

**Simple process:**

1. **Create and push a version tag:**
   ```bash
   git tag v1.2.3
   git push origin v1.2.3
   ```

2. **GitHub Actions will automatically:**
   - Trigger the build workflow
   - Replace version `1.0.0` with `1.2.3` from the tag
   - Build release binaries
   - Create NuGet package
   - Create GitHub Release with artifacts
   - Upload DLL and NuGet packages

### 3. Release Artifacts

After successful release, the following artifacts will be available:

1. **GitHub Release page:**
   - `bento-sdk-{version}.zip` - Contains DLL, PDB, and dependencies
   - `Bento.SDK.{version}.nupkg` - NuGet package

2. **GitHub Actions artifacts:**
   - Build artifacts (retained for 90 days)
   - NuGet packages (retained for 90 days)

## Configuration

The build configuration is defined in:
- `Bento/Bento.csproj` - Project settings and metadata
- `.github/workflows/build-and-publish.yml` - CI/CD workflow

For more information about configuration options, see the project files and workflow definitions.
