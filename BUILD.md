# Build and Deployment Guide

This document describes how to build the Bento .NET SDK and deploy releases.

## Prerequisites

- .NET 8.0 SDK or later
- Git
- GitHub account with appropriate permissions
- **For NuGet publishing**: NuGet.org account and API key (see [Setup](#setup))

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

## Setup

### NuGet.org Publishing Setup

To enable automatic publishing to NuGet.org, you need to configure the repository secrets:

1. **Create NuGet.org API Key:**
   - Go to [nuget.org](https://www.nuget.org/)
   - Sign in to your account
   - Go to Account Settings → API Keys
   - Create a new API key with "Push new packages and package versions" permission
   - Set glob pattern to `Bento.SDK*` for security

2. **Add GitHub Repository Secret:**
   - Go to your GitHub repository
   - Navigate to Settings → Secrets and variables → Actions
   - Click "New repository secret"
   - Name: `NUGET_API_KEY`
   - Value: Your NuGet.org API key
   - Click "Add secret"

3. **Verify Setup:**
   - Once configured, releases will automatically publish to NuGet.org
   - Check the Actions tab for publishing status
   - Package will be available at: https://www.nuget.org/packages/Bento.SDK/

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
- **On tag push**: Publishes NuGet package to NuGet.org (if API key configured)

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
   - **Publish NuGet package to NuGet.org** (if API key configured)

### 3. Release Artifacts

After successful release, the following artifacts will be available:

1. **GitHub Release page:**
   - `bento-sdk-{version}.zip` - Contains DLL, PDB, and dependencies
   - `Bento.SDK.{version}.nupkg` - NuGet package

2. **NuGet.org:**
   - Package available at: https://www.nuget.org/packages/Bento.SDK/
   - Install with: `dotnet add package Bento.SDK --version {version}`

3. **GitHub Actions artifacts:**
   - Build artifacts (retained for 90 days)
   - NuGet packages (retained for 90 days)

## Configuration

The build configuration is defined in:
- `Bento/Bento.csproj` - Project settings and metadata
- `.github/workflows/build-and-publish.yml` - CI/CD workflow

For more information about configuration options, see the project files and workflow definitions.

## Troubleshooting

### Build Failures
- Ensure .NET 8.0 SDK is installed
- Check that all dependencies are restored: `dotnet restore`
- Verify project file syntax

### Release Issues
- Ensure tag follows semantic versioning (e.g., `v1.0.0`)
- Check GitHub Actions logs for detailed error messages
- Verify repository permissions for GitHub Actions

### NuGet Publishing Issues
- Verify `NUGET_API_KEY` secret is configured in repository settings
- Check API key permissions on NuGet.org (must allow pushing)
- Ensure package ID `Bento.SDK` is available or owned by your account
- Check GitHub Actions logs for publishing errors

### Version Issues
- Tags should start with `v` and follow semantic versioning (e.g., `v1.2.3`)
- Version extraction removes the `v` prefix automatically
- AssemblyVersion adds `.0` suffix automatically (e.g., `1.2.3` → `1.2.3.0`)
- Keep base version as `1.0.0` in project file - it gets replaced automatically
