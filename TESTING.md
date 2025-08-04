# Development & Testing Guide

## Quick Start

1. **Clone and build**:
   ```bash
   git clone <repo>
   cd bento-dotnet-sdk
   dotnet build
   ```

2. **Run functional tests**:
   ```bash
   cd Bento.Examples
   dotnet run
   ```

## Testing

### Local Testing
```bash
# Run all tests
dotnet run

# Run specific test
dotnet run -- event

# Verbose output
dotnet run -- --verbose

# CI simulation (no interactive wait)
./test-ci.sh
```

### CI/CD Testing

The project includes two GitHub Actions workflows:

1. **`.github/workflows/test.yml`** - Runs on every push/PR
   - Builds the solution
   - Runs functional tests with dummy API keys
   - Verifies test execution (expects `Success=False` due to test keys)
   - Uploads test logs as artifacts

2. **`.github/workflows/build-and-publish.yml`** - Release workflow
   - Includes functional tests as part of the build
   - Creates packages and releases on version tags

### Test Configuration

For CI/CD, tests use dummy configuration:
```json
{
  "Bento": {
    "PublishableKey": "test-publishable-key",
    "SecretKey": "test-secret-key", 
    "SiteUuid": "test-site-uuid"
  }
}
```

### What Tests Verify

✅ **SDK Structure**: All services are properly registered and injectable  
✅ **API Calls**: HTTP requests are formed correctly  
✅ **Error Handling**: Services handle unauthorized responses gracefully  
✅ **Configuration**: Settings are loaded from appsettings.json  
✅ **Dependency Injection**: Service registration works correctly  

❌ **Not Tested**: Actual API responses (requires real keys)

## Development Workflow

1. Make changes to SDK code
2. Run `./test-ci.sh` to verify everything works
3. Commit and push - CI will run automatically
4. For releases, tag with `v*.*.*` format

## Debugging

Test logs are available:
- Locally: `Bento.Examples/test_output.log`
- In CI: Download from Actions artifacts
- Both contain full verbose output for debugging
