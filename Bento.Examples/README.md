# Bento .NET SDK Examples

This project demonstrates how to use the Bento .NET SDK with comprehensive tests for all available services.

## Configuration

1. Copy the `appsettings.json` file and update it with your Bento API credentials:

```json
{
  "Bento": {
    "PublishableKey": "your-actual-bento-publishable-key",
    "SecretKey": "your-actual-bento-secret-key",
    "SiteUuid": "your-actual-bento-site-uuid"
  }
}
```

## Running Tests

### Run All Tests
```bash
dotnet run
```

### Run Specific Test
```bash
dotnet run -- event
dotnet run -- subscriber
dotnet run -- tag
# etc.
```

### Run with Verbose Output
```bash
dotnet run -- event --verbose
dotnet run -- --verbose  # All tests with verbose output
```

### Run in CI/CD Mode (no interactive wait)
```bash
dotnet run -- --no-wait
CI=true dotnet run -- event --verbose  # Environment variable also works
```

### Show Help
```bash
dotnet run -- --help
```

## Available Tests

The following services are tested:

- **event** - Event tracking functionality
- **subscriber** - Subscriber management (create, find, import)
- **tag** - Tag management (get, create)
- **field** - Field management (get, create)
- **email** - Email sending functionality
- **broadcast** - Broadcast management (get, create)
- **stats** - Statistics retrieval (site, segment, report)
- **command** - Command execution
- **blacklist** - IP blacklist status checking
- **validation** - Email validation
- **moderation** - Content moderation
- **gender** - Gender prediction
- **geolocation** - IP geolocation

## Test Output

The test runner provides:
- ✅ Success indicators for passed tests
- ❌ Failure indicators for failed tests
- Execution timing for each test
- Summary with pass/fail counts
- Detailed error information in verbose mode

## Notes

- Tests will show `Success=False` with unauthorized responses if API keys are not properly configured
- All tests are designed to be safe and won't affect your production data
- The test runner demonstrates proper error handling and logging patterns

## CI/CD Integration

The functional tests are automatically executed in GitHub Actions workflows:

- **test.yml** - Runs on every push/PR for quick feedback
- **build-and-publish.yml** - Includes tests as part of the release process

The CI/CD pipeline:
1. Creates test configuration with dummy API keys
2. Runs all functional tests with `--verbose --no-wait` flags
3. Verifies that tests execute properly (expecting `Success=False` due to test keys)
4. Uploads test logs as artifacts for debugging

To run tests locally in CI mode:
```bash
CI=true dotnet run -- --verbose --no-wait
```
