using Bento.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bento.Examples;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Bento .NET SDK Test Runner");
        Console.WriteLine("=========================\n");

        // Show help if requested
        if (args.Contains("--help") || args.Contains("-h"))
        {
            ShowHelp();
            return;
        }

        // Parse command line arguments
        var testFilter = args.Length > 0 && !args[0].StartsWith("--") ? args[0] : null;
        var verbose = args.Contains("--verbose") || args.Contains("-v");
        var noWait = args.Contains("--no-wait") || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        // Check if API keys are configured
        var publishableKey = configuration["Bento:PublishableKey"];
        var secretKey = configuration["Bento:SecretKey"];
        var siteUuid = configuration["Bento:SiteUuid"];

        if (string.IsNullOrEmpty(publishableKey) || publishableKey == "your-bento-publishable-key" ||
            string.IsNullOrEmpty(secretKey) || secretKey == "your-bento-secret-key" ||
            string.IsNullOrEmpty(siteUuid) || siteUuid == "your-bento-site-uuid")
        {
            Console.WriteLine("⚠️  Warning: Bento API keys not configured properly.");
            Console.WriteLine("   Please update appsettings.json with your real API keys.");
            Console.WriteLine("   Tests will run but will return 'Unauthorized' responses.\n");
        }

        var services = new ServiceCollection();

        // Register all Bento services
        services.AddBentoClient(configuration);

        // Register the example service
        services.AddScoped<BentoExampleService>();

        var serviceProvider = services.BuildServiceProvider();

        try
        {
            var exampleService = serviceProvider.GetRequiredService<BentoExampleService>();
            
            if (string.IsNullOrEmpty(testFilter))
            {
                Console.WriteLine("Running all tests...\n");
                await exampleService.RunExamples(verbose);
            }
            else
            {
                Console.WriteLine($"Running filtered tests: {testFilter}\n");
                await exampleService.RunSpecificTest(testFilter, verbose);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical Error: {ex.Message}");
            if (verbose)
            {
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            Environment.Exit(1);
        }
        
        if (!noWait)
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Usage: dotnet run [test_name] [options]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  test_name    Run specific test (optional)");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --verbose, -v    Enable verbose output");
        Console.WriteLine("  --no-wait        Don't wait for key press (useful for CI/CD)");
        Console.WriteLine("  --help, -h       Show this help message");
        Console.WriteLine();
        Console.WriteLine("Available tests:");
        Console.WriteLine("  event, subscriber, tag, field, email, broadcast,");
        Console.WriteLine("  stats, command, blacklist, validation, moderation,");
        Console.WriteLine("  gender, geolocation");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  dotnet run                    # Run all tests");
        Console.WriteLine("  dotnet run event              # Run only event test");
        Console.WriteLine("  dotnet run event --verbose    # Run event test with verbose output");
    }
}