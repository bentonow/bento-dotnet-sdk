using System;
using System.Collections.Generic;
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

    public async Task RunExamples(bool verbose = false)
    {
        var testResults = new Dictionary<string, bool>();
        
        try
        {
            Console.WriteLine("Running Bento SDK Tests...\n");

            testResults["Event"] = await RunTestSafely("Event", RunEventExample, verbose);
            testResults["Subscriber"] = await RunTestSafely("Subscriber", RunSubscriberExample, verbose);
            testResults["Tag"] = await RunTestSafely("Tag", RunTagExample, verbose);
            testResults["Field"] = await RunTestSafely("Field", RunFieldExample, verbose);
            testResults["Email"] = await RunTestSafely("Email", RunEmailExample, verbose);
            testResults["Broadcast"] = await RunTestSafely("Broadcast", RunBroadcastExample, verbose);
            testResults["Stats"] = await RunTestSafely("Stats", RunStatsExample, verbose);
            testResults["Command"] = await RunTestSafely("Command", RunCommandExample, verbose);
            testResults["Blacklist"] = await RunTestSafely("Blacklist", RunBlacklistExample, verbose);
            testResults["Validation"] = await RunTestSafely("Validation", RunValidationExample, verbose);
            testResults["Moderation"] = await RunTestSafely("Moderation", RunModerationExample, verbose);
            testResults["Gender"] = await RunTestSafely("Gender", RunGenderExample, verbose);
            testResults["Geolocation"] = await RunTestSafely("Geolocation", RunGeolocationExample, verbose);

            // Summary
            var passed = testResults.Count(r => r.Value);
            var failed = testResults.Count(r => !r.Value);
            
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"Test Summary: {passed} passed, {failed} failed");
            Console.WriteLine(new string('=', 50));
            
            if (failed > 0)
            {
                Console.WriteLine("\nFailed tests:");
                foreach (var test in testResults.Where(r => !r.Value))
                {
                    Console.WriteLine($"  ❌ {test.Key}");
                }
            }
            
            if (passed > 0)
            {
                Console.WriteLine("\nPassed tests:");
                foreach (var test in testResults.Where(r => r.Value))
                {
                    Console.WriteLine($"  ✅ {test.Key}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error running tests: {ex.Message}");
            if (verbose)
            {
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }

    public async Task RunSpecificTest(string testName, bool verbose = false)
    {
        var testName_ = testName.ToLowerInvariant();
        
        Func<Task>? testAction = testName_ switch
        {
            "event" => RunEventExample,
            "subscriber" => RunSubscriberExample,
            "tag" => RunTagExample,
            "field" => RunFieldExample,
            "email" => RunEmailExample,
            "broadcast" => RunBroadcastExample,
            "stats" => RunStatsExample,
            "command" => RunCommandExample,
            "blacklist" => RunBlacklistExample,
            "validation" => RunValidationExample,
            "moderation" => RunModerationExample,
            "gender" => RunGenderExample,
            "geolocation" => RunGeolocationExample,
            _ => null
        };

        if (testAction == null)
        {
            Console.WriteLine($"Unknown test: {testName}");
            Console.WriteLine("Available tests: event, subscriber, tag, field, email, broadcast, stats, command, blacklist, validation, moderation, gender, geolocation");
            return;
        }

        await RunTestSafely(testName, testAction, verbose);
    }

    private async Task<bool> RunTestSafely(string testName, Func<Task> testAction, bool verbose)
    {
        try
        {
            Console.WriteLine($"Running {testName} test...");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            await testAction();
            
            stopwatch.Stop();
            Console.WriteLine($"✅ {testName} test completed successfully in {stopwatch.ElapsedMilliseconds}ms\n");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ {testName} test failed: {ex.Message}");
            if (verbose)
            {
                Console.WriteLine($"   Stack trace: {ex.StackTrace}");
            }
            Console.WriteLine();
            return false;
        }
    }
    
    private async Task RunCommandExample()
    {
        Console.WriteLine("  → Testing command execution...");

        // 1. Execute single command (generic response)
        var addTagCommand = CommandRequestHelper.AddTag("test@example.com", "sdk:dotnet");
        var singleResponse = await _commandService.ExecuteCommandAsync<dynamic>(addTagCommand);
        Console.WriteLine($"  → Execute single command (generic): Success={singleResponse.Success}");

        // 2. Execute single command (typed response) - returns SubscriberResponse directly
        try
        {
            var removeTagCommand = CommandRequestHelper.RemoveTag("test@example.com", "old-tag");
            var singleTypedResponse = await _commandService.ExecuteCommandAsync(removeTagCommand);
            Console.WriteLine($"  → Execute single command (typed): Updated subscriber with ID={singleTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Execute single command (typed): Failed - {ex.Message}");
        }

        // 3. Execute batch commands (generic response)
        var batchCommands = new[]
        {
            CommandRequestHelper.AddField("test@example.com", "test_field", "test_value"),
            CommandRequestHelper.Subscribe("test@example.com"),
            CommandRequestHelper.AddTagViaEvent("test@example.com", "via-event-tag")
        };
        
        var batchResponse = await _commandService.ExecuteBatchCommandsAsync<dynamic>(batchCommands);
        Console.WriteLine($"  → Execute batch commands (generic): Success={batchResponse.Success}");

        // 4. Execute batch commands (typed response) - returns SubscriberResponse directly
        try
        {
            var batchTypedResponse = await _commandService.ExecuteBatchCommandsAsync(batchCommands);
            Console.WriteLine($"  → Execute batch commands (typed): Updated subscriber with ID={batchTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Execute batch commands (typed): Failed - {ex.Message}");
        }

        // 5. Demonstrate all command types
        var allCommandTypes = new[]
        {
            CommandRequestHelper.AddTag("test@example.com", "new-tag"),
            CommandRequestHelper.AddTagViaEvent("test@example.com", "event-tag"),
            CommandRequestHelper.RemoveTag("test@example.com", "remove-tag"),
            CommandRequestHelper.AddField("test@example.com", "custom_field", "custom_value"),
            CommandRequestHelper.RemoveField("test@example.com", "old_field"),
            CommandRequestHelper.Subscribe("test@example.com"),
            CommandRequestHelper.Unsubscribe("test@example.com"),
            CommandRequestHelper.ChangeEmail("test@example.com", "new@example.com")
        };
        
        var allCommandsResponse = await _commandService.ExecuteBatchCommandsAsync<dynamic>(allCommandTypes);
        Console.WriteLine($"  → Execute all command types: Success={allCommandsResponse.Success}");
    }

    private async Task RunBlacklistExample()
    {
        Console.WriteLine("\nTesting Blacklist Service:");

        // Check domain and IP
        var request = new BlacklistStatusRequest(
            Domain: "example.com",
            IpAddress: "1.1.1.1"
        );

        // Generic response
        var response = await _blacklistService.GetBlacklistStatusAsync<dynamic>(request);
        Console.WriteLine($"Blacklist status response: {response.Success}");

        // Typed response with detailed results
        try
        {
            var blacklistResult = await _blacklistService.GetBlacklistStatusAsync(request);
            Console.WriteLine($"Blacklist check for: {blacklistResult.Query}");
            Console.WriteLine($"Description: {blacklistResult.Description}");
            
            if (blacklistResult.Results != null)
            {
                Console.WriteLine($"Spamhaus: {blacklistResult.Results.Spamhaus}");
                Console.WriteLine($"Just Registered: {blacklistResult.Results.JustRegistered}");
                Console.WriteLine($"Nordspam: {blacklistResult.Results.Nordspam}");
                Console.WriteLine($"Abusix: {blacklistResult.Results.Abusix}");
            }
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"Blacklist check failed: {ex.Message}");
        }
    }

    private async Task RunValidationExample()
    {
        Console.WriteLine("\nTesting Email Validation Service:");

        // Basic email validation
        var request = new EmailValidationRequest(
            EmailAddress: "john@example.com",
            FullName: "John Doe",
            IpAddress: "1.1.1.1"
        );

        var response = await _validationService.ValidateEmailAsync<dynamic>(request);
        Console.WriteLine($"Email validation response: {response.Success}");

        // Jesse's ruleset validation
        var jesseRequest = new JesseRulesetRequest
        {
            EmailAddress = "test@gmail.com",
            BlockFreeProviders = true,
            Wiggleroom = false
        };
        
        try
        {
            var jesseResponse = await _validationService.ValidateEmailWithJesseRulesetAsync(jesseRequest);
            Console.WriteLine($"Jesse's ruleset validation - Valid: {jesseResponse.Valid}");
            if (jesseResponse.Reasons != null)
                Console.WriteLine($"Reasons: {string.Join(", ", jesseResponse.Reasons)}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"Jesse's validation failed: {ex.Message}");
        }
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

    private async Task RunBroadcastExample()
    {
        Console.WriteLine("  → Testing broadcast service...");

        // Get all broadcasts
        var getBroadcastsResponse = await _broadcastService.GetBroadcastsAsync<dynamic>();
        Console.WriteLine($"  → Get broadcasts: Success={getBroadcastsResponse.Success}");

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
        Console.WriteLine($"  → Create broadcast: Success={createBroadcastResponse.Success}");
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

    private async Task RunEventExample()
    {
        Console.WriteLine("  → Testing event tracking...");

        // 1. Track single event (generic response)
        var eventRequest = new EventRequest(
            Type: "$completed_onboarding",
            Email: "test@example.com",
            Fields: new Dictionary<string, object>
            {
                { "first_name", "John" },
                { "last_name", "Doe" }
            },
            Details: new Dictionary<string, object>
            {
                { "test_detail", "test_value" }
            }
        );

        var genericResponse = await _eventService.TrackEventAsync<dynamic>(eventRequest);
        Console.WriteLine($"  → Event tracked (generic): Success={genericResponse.Success}, Status={genericResponse.StatusCode}");

        // 2. Track single event (typed response) - returns EventResponse directly
        try
        {
            var typedResponse = await _eventService.TrackEventAsync(eventRequest);
            Console.WriteLine($"  → Event tracked (typed): Results={typedResponse.Results}, Failed={typedResponse.Failed}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  → Event tracking failed: {ex.Message}");
        }

        // 3. Track multiple events in batch
        var batchEvents = new[]
        {
            new EventRequest("$purchase", "buyer1@example.com", 
                Fields: new Dictionary<string, object> { { "amount", 99.99 } }),
            new EventRequest("$purchase", "buyer2@example.com", 
                Fields: new Dictionary<string, object> { { "amount", 149.99 } })
        };

        try
        {
            var batchResponse = await _eventService.TrackEventsAsync(batchEvents);
            Console.WriteLine($"  → Batch events tracked: Results={batchResponse.Results}, Failed={batchResponse.Failed}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  → Batch event tracking failed: {ex.Message}");
        }
    }

    private async Task RunSubscriberExample()
    {
        Console.WriteLine("  → Testing subscriber management...");

        // 1. Find subscriber (generic response)
        var findResponse = await _subscriberService.FindSubscriberAsync<dynamic>("test@example.com");
        Console.WriteLine($"  → Find subscriber (generic): Success={findResponse.Success}");

        // 2. Find subscriber (typed response) - returns SubscriberResponse directly
        try
        {
            var findTypedResponse = await _subscriberService.FindSubscriberAsync("test@example.com");
            Console.WriteLine($"  → Find subscriber (typed): Found subscriber with ID={findTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Find subscriber (typed): Failed - {ex.Message}");
        }

        // 3. Find subscriber with request object
        try
        {
            var findRequest = new FindSubscriberRequest { Email = "test@example.com" };
            var findRequestResponse = await _subscriberService.FindSubscriberAsync(findRequest);
            Console.WriteLine($"  → Find subscriber (request): Found subscriber with ID={findRequestResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Find subscriber (request): Failed - {ex.Message}");
        }

        // 4. Create subscriber (generic response)
        var subscriberRequest = new SubscriberRequest(
            Email: "test@example.com",
            FirstName: "Test",
            LastName: "User",
            Tags: new[] { "test", "sdk", "sdk:dotnet" }
        );
        
        var createResponse = await _subscriberService.CreateSubscriberAsync<dynamic>(subscriberRequest);
        Console.WriteLine($"  → Create subscriber (generic): Success={createResponse.Success}");

        // 5. Create subscriber (typed response) - returns SubscriberResponse directly
        try
        {
            var createTypedResponse = await _subscriberService.CreateSubscriberAsync(subscriberRequest);
            Console.WriteLine($"  → Create subscriber (typed): Created subscriber with ID={createTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Create subscriber (typed): Failed - {ex.Message}");
        }

        // 6. Import subscribers (generic response)
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
        Console.WriteLine($"  → Import {subscribers.Count} subscribers (generic): Success={importResponse.Success}");

        // 7. Import subscribers (typed response) - returns ImportSubscribersResponse directly
        try
        {
            var importTypedResponse = await _subscriberService.ImportSubscribersAsync(subscribers);
            Console.WriteLine($"  → Import {subscribers.Count} subscribers (typed): Processed {importTypedResponse.Result} subscribers");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Import {subscribers.Count} subscribers (typed): Failed - {ex.Message}");
        }

        // 8. Search subscribers (Enterprise feature)
        var searchRequest = new SearchSubscribersRequest
        {
            Page = 1,
            CreatedAt = new DateFilter { Gt = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
        };
        
        var searchResponse = await _subscriberService.SearchSubscribersAsync<dynamic>(searchRequest);
        Console.WriteLine($"  → Search subscribers (generic): Success={searchResponse.Success}");

        // 9. Search subscribers (typed response) - returns SearchSubscribersResponse directly
        try
        {
            var searchTypedResponse = await _subscriberService.SearchSubscribersAsync(searchRequest);
            Console.WriteLine($"  → Search subscribers (typed): Found {searchTypedResponse.Data?.Count() ?? 0} subscribers");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Search subscribers (typed): Failed - {ex.Message}");
        }

        // 10. Run single command (add tag)
        var addTagCommand = CommandRequestHelper.AddTag("test@example.com", "new-tag");
        var commandResponse = await _subscriberService.RunCommandAsync<dynamic>(addTagCommand);
        Console.WriteLine($"  → Run add tag command (generic): Success={commandResponse.Success}");

        // 11. Run single command (typed response) - returns SubscriberResponse directly
        try
        {
            var removeTagCommand = CommandRequestHelper.RemoveTag("test@example.com", "old-tag");
            var commandTypedResponse = await _subscriberService.RunCommandAsync(removeTagCommand);
            Console.WriteLine($"  → Run remove tag command (typed): Updated subscriber with ID={commandTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Run remove tag command (typed): Failed - {ex.Message}");
        }

        // 12. Run multiple commands
        var batchCommands = new[]
        {
            CommandRequestHelper.AddField("test@example.com", "test_field", "test_value"),
            CommandRequestHelper.Subscribe("test@example.com"),
            CommandRequestHelper.AddTagViaEvent("test@example.com", "via-event-tag")
        };
        
        var batchResponse = await _subscriberService.RunCommandsAsync<dynamic>(batchCommands);
        Console.WriteLine($"  → Run batch commands (generic): Success={batchResponse.Success}");

        // 13. Run multiple commands (typed response) - returns SubscriberResponse directly
        try
        {
            var batchTypedResponse = await _subscriberService.RunCommandsAsync(batchCommands);
            Console.WriteLine($"  → Run batch commands (typed): Updated subscriber with ID={batchTypedResponse.Id}");
        }
        catch (BentoException ex)
        {
            Console.WriteLine($"  → Run batch commands (typed): Failed - {ex.Message}");
        }
    }

    private async Task RunTagExample()
    {
        Console.WriteLine("  → Testing tag management...");

        // Get all tags
        var getTagsResponse = await _tagService.GetTagsAsync<dynamic>();
        Console.WriteLine($"  → Get tags: Success={getTagsResponse.Success}");

        // Create a new tag
        var tagRequest = new TagRequest("sdk:dotnet");
        var createTagResponse = await _tagService.CreateTagAsync<dynamic>(tagRequest);
        Console.WriteLine($"  → Create tag 'sdk:dotnet': Success={createTagResponse.Success}");
    }

    private async Task RunFieldExample()
    {
        Console.WriteLine("  → Testing field management...");

        // Get all fields
        var getFieldsResponse = await _fieldService.GetFieldsAsync<dynamic>();
        Console.WriteLine($"  → Get fields: Success={getFieldsResponse.Success}");

        // Create a new field
        var fieldRequest = new FieldRequest("dotnet_test_field");
        var createFieldResponse = await _fieldService.CreateFieldAsync<dynamic>(fieldRequest);
        Console.WriteLine($"  → Create field 'dotnet_test_field': Success={createFieldResponse.Success}");
    }

    private async Task RunEmailExample()
    {
        Console.WriteLine("  → Testing email service...");

        var emailRequest = new EmailRequest(
            From: "Your Bento Author",
            Subject: "Test Email from .NET SDK",
            HtmlBody: "<p>Hello from the .NET SDK!</p>",
            To: "A subscriber email"
        );

        var response = await _emailService.SendEmailAsync<dynamic>(emailRequest);
        Console.WriteLine($"  → Send email: Success={response.Success}");
    }
}