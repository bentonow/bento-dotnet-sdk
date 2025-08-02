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
   dotnet build Bento/Bento.csproj --configuration Release --output ./artifacts
   ```

### Running Tests

Tests will run automatically if test projects are detected:
```bash
dotnet test --configuration Release --verbosity normal
```

### Creating NuGet Package

```bash
dotnet pack Bento/Bento.csproj --configuration Release --output ./packages
```

## Automated CI/CD

The project uses GitHub Actions for automated builds and deployments. There are two main workflows:

### 1. Build and Publish (`build-and-publish.yml`)

**Triggers:**
- Push to `main` or `develop` branches
- Pull requests to `main` branch  
- Push of version tags (e.g., `v1.2.3`)

**What it does:**
- Builds the solution
- Runs tests (if available)
- Creates DLL artifacts for regular builds
- **On tag push**: Replaces version `1.0.0` with tag version, creates release with artifacts

### 2. Manual Release Publisher (`publish-release.yml`)

**Triggers:**
- Manual workflow dispatch with version input

**What it does:**
- Validates version format
- Creates and pushes git tag
- Builds release with specified version
- Creates GitHub Release with artifacts

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

**Option A: Automatic Release (Recommended)**

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

**Option B: Manual Release**

1. **Go to GitHub Actions tab**
2. **Select "Manual Release Publisher" workflow**
3. **Click "Run workflow"**
4. **Enter version (e.g., `1.2.3`)**
5. **The workflow will:**
   - Create and push the tag automatically
   - Build and publish the release

### 3. Release Artifacts

After successful release, the following artifacts will be available:

1. **GitHub Release page:**
   - `bento-sdk-{version}.zip` - Contains DLL, PDB, and dependencies
   - `Bento.SDK.{version}.nupkg` - NuGet package

2. **GitHub Actions artifacts:**
   - Build artifacts (retained for 90 days)
   - NuGet packages (retained for 90 days)

## Manual Release (if needed)

If you need to create a release manually:

1. **Update version in project file:**
   ```xml
   <Version>1.0.0</Version>
   <AssemblyVersion>1.0.0.0</AssemblyVersion>
   <FileVersion>1.0.0.0</FileVersion>
   ```

2. **Build release artifacts:**
   ```bash
   dotnet build Bento/Bento.csproj --configuration Release --output ./release-artifacts
   ```

3. **Create NuGet package:**
   ```bash
   dotnet pack Bento/Bento.csproj --configuration Release --output ./packages
   ```

4. **Create ZIP archive:**
   ```bash
   cd release-artifacts
   zip -r ../bento-sdk-1.0.0.zip Bento.dll Bento.pdb Bento.deps.json
   ```

## Artifacts Description

### DLL Package (`bento-sdk-{version}.zip`)
Contains:
- `Bento.dll` - Main library
- `Bento.pdb` - Debug symbols
- `Bento.deps.json` - Dependency information

### NuGet Package (`Bento.SDK.{version}.nupkg`)
- Ready-to-use NuGet package
- Can be published to NuGet.org or private feeds
- Includes all dependencies and metadata

## Troubleshooting

### Build Failures
- Ensure .NET 8.0 SDK is installed
- Check that all dependencies are restored: `dotnet restore`
- Verify project file syntax

### Release Issues
- Ensure tag follows semantic versioning (e.g., `v1.0.0`)
- Check GitHub Actions logs for detailed error messages
- Verify repository permissions for GitHub Actions

### Version Issues
- Tags should start with `v` and follow semantic versioning (e.g., `v1.2.3`)
- Version extraction removes the `v` prefix automatically
- AssemblyVersion adds `.0` suffix automatically (e.g., `1.2.3` → `1.2.3.0`)
- Keep base version as `1.0.0` in project file - it gets replaced automatically

## Configuration

The build configuration is defined in:
- `Bento/Bento.csproj` - Project settings and metadata
- `.github/workflows/build-and-publish.yml` - Main CI/CD workflow
- `.github/workflows/publish-release.yml` - Release workflow

For more information about configuration options, see the project files and workflow definitions.
