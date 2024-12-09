using Bento.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bento.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBentoClient(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<BentoOptions>(
            configuration.GetSection(BentoOptions.SectionName));

        services.AddHttpClient<IBentoClient, BentoClient>();
        
        // Register all Bento services
        services.AddScoped<IBentoEventService, BentoEventService>();
        services.AddScoped<IBentoSubscriberService, BentoSubscriberService>();
        services.AddScoped<IBentoTagService, BentoTagService>();
        services.AddScoped<IBentoFieldService, BentoFieldService>();
        services.AddScoped<IBentoEmailService, BentoEmailService>();
        services.AddScoped<IBentoBroadcastService, BentoBroadcastService>();
        services.AddScoped<IBentoStatsService, BentoStatsService>();
        services.AddScoped<IBentoCommandService, BentoCommandService>();
        services.AddScoped<IBentoBlacklistService, BentoBlacklistService>();
        services.AddScoped<IBentoValidationService, BentoValidationService>();
        services.AddScoped<IBentoModerationService, BentoModerationService>();
        services.AddScoped<IBentoGenderService, BentoGenderService>();
        services.AddScoped<IBentoGeolocationService, BentoGeolocationService>();
        
        return services;
    }
}