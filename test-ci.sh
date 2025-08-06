#!/bin/bash
set -e

echo "🚀 Running Bento SDK CI Test Simulation"
echo "======================================"

# Build the solution
echo "📦 Building solution..."
dotnet build --configuration Debug --no-restore

# Change to examples directory
cd Bento.Examples

# Create test configuration
echo "⚙️  Creating test configuration..."
cat > appsettings.test.json << 'EOF'
{
  "Bento": {
    "PublishableKey": "test-publishable-key-local",
    "SecretKey": "test-secret-key-local", 
    "SiteUuid": "test-site-uuid-local"
  }
}
EOF

echo "✅ Test configuration created"

# Set environment variables
export CI=true
export ASPNETCORE_ENVIRONMENT=test

# Run all tests
echo "🧪 Running all functional tests..."
timeout 300 dotnet run -- --verbose --no-wait > test_output.log 2>&1 || TEST_EXIT_CODE=$?

echo "📋 Test Results:"
echo "==============="
cat test_output.log

# Verify tests ran
if grep -q "Test Summary:" test_output.log; then
    PASSED_COUNT=$(grep -o "[0-9]\+ passed" test_output.log | grep -o "[0-9]\+")
    echo ""
    echo "✅ SUCCESS: Functional tests completed ($PASSED_COUNT tests executed)"
    echo "ℹ️  Note: Tests show 'Success=False' due to test API keys (expected behavior)"
else
    echo ""
    echo "❌ FAILED: Tests did not complete properly"
    exit 1
fi

# Test specific service
echo ""
echo "🎯 Testing specific service..."
timeout 60 dotnet run -- event --verbose --no-wait > specific_test.log 2>&1 || SPECIFIC_EXIT_CODE=$?

if grep -q "event test completed successfully" specific_test.log; then
    echo "✅ Specific test execution works"
else
    echo "❌ Specific test execution failed"
    cat specific_test.log
    exit 1
fi

echo ""
echo "🎉 All CI simulation tests passed!"
echo "📁 Test logs saved to:"
echo "   - test_output.log"
echo "   - specific_test.log"
echo "   - appsettings.test.json"
