using Bento.Models;
using Bento.Services;

namespace Bento.Examples;

public class BentoExampleService
{
    private readonly IBentoEventService _eventService;
    private readonly IBentoSubscriberService _subscriberService;
    private readonly IBentoTagService _tagService;
    private readonly IBentoFieldService _fieldService;
    private readonly IBentoEmailService _emailService;
    private readonly IBentoBroadcastService _broadcastService;
    private readonly IBentoStatsService _statsService;
    private readonly IBentoCommandService _commandService;
    private readonly IBentoBlacklistService _blacklistService;
    private readonly IBentoValidationService _validationService;
    private readonly IBentoModerationService _moderationService;
    private readonly IBentoGenderService _genderService;
    private readonly IBentoGeolocationService _geolocationService;

    public BentoExampleService(
        IBentoEventService eventService,
        IBentoSubscriberService subscriberService,
        IBentoTagService tagService,
        IBentoFieldService fieldService,
        IBentoEmailService emailService,
        IBentoBroadcastService broadcastService,
        IBentoStatsService statsService,
        IBentoCommandService commandService,
        IBentoBlacklistService blacklistService,
        IBentoValidationService validationService,
        IBentoModerationService moderationService,
        IBentoGenderService genderService,
        IBentoGeolocationService geolocationService)
    {
        _eventService = eventService;
        _subscriberService = subscriberService;
        _tagService = tagService;
        _fieldService = fieldService;
        _emailService = emailService;
        _broadcastService = broadcastService;
        _statsService = statsService;
        _commandService = commandService;
        _blacklistService = blacklistService;
        _validationService = validationService;
        _moderationService = moderationService;
        _genderService = genderService;
        _geolocationService = geolocationService;
    }

    public async Task RunExamples()
    {
        try
        {
            Console.WriteLine("Running Bento SDK Examples...\n");

            await RunEventExample();
            await RunSubscriberExample();
            await RunTagExample();
            await RunFieldExample();
            await RunEmailExample();
            await RunBroadcastExample();
            await RunStatsExample();
            await RunCommandExample();
            await RunBlacklistExample();
            await RunValidationExample();
            await RunModerationExample();
            await RunGenderExample();
            await RunGeolocationExample();
            

            Console.WriteLine("\nAll examples completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running examples: {ex.Message}");
        }
    }
    
    private async Task RunCommandExample()
    {
        Console.WriteLine("\nTesting Command Service:");

        var command = new CommandRequest(
            Command: "add_tag",
            Email: "test@example.com",
            Query: "sdk:dotnet"
        );

        var response = await _commandService.ExecuteCommandAsync<dynamic>(command);
        Console.WriteLine($"Command execution response: {response.Success}");
    }

    private async Task RunBlacklistExample()
    {
        Console.WriteLine("\nTesting Blacklist Service:");

        var request = new BlacklistStatusRequest(
            IpAddress: "1.1.1.1"
        );

        var response = await _blacklistService.GetBlacklistStatusAsync<dynamic>(request);
        Console.WriteLine($"Blacklist status response: {response.Success}");
    }

    private async Task RunValidationExample()
    {
        Console.WriteLine("\nTesting Email Validation Service:");

        var request = new EmailValidationRequest(
            EmailAddress: "john@example.com",
            FullName: "John Doe",
            IpAddress: "1.1.1.1"
        );

        var response = await _validationService.ValidateEmailAsync<dynamic>(request);
        Console.WriteLine($"Email validation response: {response.Success}");
    }

    private async Task RunModerationExample()
    {
        Console.WriteLine("\nTesting Content Moderation Service:");

        var request = new ContentModerationRequest(
            Content: "Hello World!"
        );

        var response = await _moderationService.ModerateContentAsync<dynamic>(request);
        Console.WriteLine($"Content moderation response: {response.Success}");
    }

    private async Task RunGenderExample()
    {
        Console.WriteLine("\nTesting Gender Prediction Service:");

        var request = new GenderRequest(
            FullName: "John Doe"
        );

        var response = await _genderService.PredictGenderAsync<dynamic>(request);
        Console.WriteLine($"Gender prediction response: {response.Success}");
    }

    private async Task RunGeolocationExample()
    {
        Console.WriteLine("\nTesting IP Geolocation Service:");

        var request = new GeolocationRequest(
            IpAddress: "1.1.1.1"
        );

        var response = await _geolocationService.GeolocateIpAsync<dynamic>(request);
        Console.WriteLine($"IP geolocation response: {response.Success}");
    }
    
    private async Task RunEmailExample()
    {
        Console.WriteLine("\nTesting Email Service:");

        var emailRequest = new EmailRequest(
            From: "Your Bento Author",
            Subject: "Test Email from .NET SDK",
            HtmlBody: "<p>Hello from the .NET SDK!</p>",
            To: "A subscriber email"
        );

        var response = await _emailService.SendEmailAsync<dynamic>(emailRequest);
        Console.WriteLine($"Send email response: {response.Success}");
    }

    private async Task RunBroadcastExample()
    {
        Console.WriteLine("\nTesting Broadcast Service:");

        // Get all broadcasts
        var getBroadcastsResponse = await _broadcastService.GetBroadcastsAsync<dynamic>();
        Console.WriteLine($"Get broadcasts response: {getBroadcastsResponse.Success}");

        // Create a new broadcast
        var broadcastRequest = new BroadcastRequest(
            Name: "Test Broadcast from .NET SDK",
            Subject: "Test Broadcast Subject",
            Content: "<p>Hello from the .NET SDK Broadcast!</p>",
            Type: "plain",
            From: new ContactInfo(
                EmailAddress: "Your Author",
                Name: "Author Name"
            ),
            InclusiveTags: "test",
            BatchSizePerHour: 1000
        );

        var createBroadcastResponse = await _broadcastService.CreateBroadcastAsync<dynamic>(broadcastRequest);
        Console.WriteLine($"Create broadcast response: {createBroadcastResponse.Success}");
    }

    private async Task RunStatsExample()
    {
        Console.WriteLine("\nTesting Stats Service:");

        // Get site stats
        var siteStatsResponse = await _statsService.GetSiteStatsAsync<dynamic>();
        Console.WriteLine($"Get site stats response: {siteStatsResponse.Success}");

        // Get segment stats
        var segmentStatsResponse = await _statsService.GetSegmentStatsAsync<dynamic>("segment_ID");
        Console.WriteLine($"Get segment stats response: {segmentStatsResponse.Success}");

        // Get report stats
        var reportStatsResponse = await _statsService.GetReportStatsAsync<dynamic>("report_ID");
        Console.WriteLine($"Get report stats response: {reportStatsResponse.Success}");
    }
    
    private async Task RunFieldExample()
    {
        Console.WriteLine("\nTesting Field Management:");

        // Get all fields
        var getFieldsResponse = await _fieldService.GetFieldsAsync<dynamic>();
        Console.WriteLine($"Get fields response: {getFieldsResponse.Success}");

        // Create a new field
        var fieldRequest = new FieldRequest("dotnet_test_field");
        var createFieldResponse = await _fieldService.CreateFieldAsync<dynamic>(fieldRequest);
        Console.WriteLine($"Create field response: {createFieldResponse.Success}");
    }

    private async Task RunEventExample()
    {
        Console.WriteLine("Testing Event Tracking:");

        var eventRequest = new EventRequest(
            Type: "DotNet_test_event",
            Email: "test@example.com",
            Fields: new Dictionary<string, object>
            {
                { "test_field", "test_value" }
            },
            Details: new Dictionary<string, object>
            {
                { "test_detail", "test_value" }
            }
        );

        var response = await _eventService.TrackEventAsync<dynamic>(eventRequest);
        Console.WriteLine($"Event tracked successfully: {response.Success}");
        Console.WriteLine($"Status: {response.StatusCode}");
    }

    private async Task RunSubscriberExample()
    {
        Console.WriteLine("\nTesting Subscriber Management:");

        // Find subscriber
        var findResponse = await _subscriberService.FindSubscriberAsync<dynamic>("test@example.com");
        Console.WriteLine($"Find subscriber response: {findResponse.Success}");

        // Create subscriber
        var subscriberRequest = new SubscriberRequest(
            Email: "test@example.com",
            FirstName: "Test",
            LastName: "User",
            Tags: new[] { "test", "sdk", "sdk:dotnet" }
        );
        
        var createResponse = await _subscriberService.CreateSubscriberAsync<dynamic>(subscriberRequest);
        Console.WriteLine($"Create subscriber response: {createResponse.Success}");

        // Import subscribers
        var subscribers = new List<SubscriberRequest>
        {
            new(
                Email: "first@example.com",
                FirstName: "John",
                LastName: "Doe",
                Tags: new[] { "test", "sdk", "sdk:dotnet" }
            ),
            new(
                Email: "second@example.com",
                FirstName: "Jane",
                LastName: "Doe",
                Tags: new[] { "test", "sdk", "sdk:dotnet" }
            )
        };
        
        var importResponse = await _subscriberService.ImportSubscribersAsync<dynamic>(subscribers);
        Console.WriteLine($"Import subscribers response: {importResponse.Success}");
    }

    private async Task RunTagExample()
    {
        Console.WriteLine("\nTesting Tag Management:");

        // Get all tags
        var getTagsResponse = await _tagService.GetTagsAsync<dynamic>();
        Console.WriteLine($"Get tags response: {getTagsResponse.Success}");

        // Create a new tag
        var tagRequest = new TagRequest("sdk:dotnet");
        var createTagResponse = await _tagService.CreateTagAsync<dynamic>(tagRequest);
        Console.WriteLine($"Create tag response: {createTagResponse.Success}");
    }
}