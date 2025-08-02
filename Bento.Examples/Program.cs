using Bento.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bento.Examples;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();

        // Register all Bento services
        services.AddBentoClient(configuration);

        // Register the example service
        services.AddScoped<BentoExampleService>();

        var serviceProvider = services.BuildServiceProvider();

        try
        {
            var exampleService = serviceProvider.GetRequiredService<BentoExampleService>();
            await exampleService.RunExamples();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}