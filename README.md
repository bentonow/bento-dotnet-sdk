# Bento .NET SDK

[![NuGet Version](https://img.shields.io/nuget/v/Bento.SDK.svg)](https://www.nuget.org/packages/Bento.SDK/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Bento.SDK.svg)](https://www.nuget.org/packages/Bento.SDK/)
[![GitHub Release](https://img.shields.io/github/release/bentonow/bento-dotnet-sdk.svg)](https://github.com/bentonow/bento-dotnet-sdk/releases)

<img align="right" src="https://app.bentonow.com/brand/logoanim.gif">

### Installation

#### Option 1: NuGet Package (Recommended)

Install the SDK via NuGet Package Manager:

```bash
dotnet add package Bento.SDK
```

Or install a specific version:
```bash
dotnet add package Bento.SDK --version 1.0.1
```

Or via Package Manager Console in Visual Studio:
```powershell
Install-Package Bento.SDK
```

#### Option 2: Download DLL

1. Download the latest release from [GitHub Releases](https://github.com/bentonow/bento-dotnet-sdk/releases)
2. Extract the ZIP file containing the SDK  
3. Add a reference to the SDK in your project file (.csproj):

> [!TIP]
> Need help? Join our [Discord](https://discord.gg/ssXXFRmt5F) or email jesse@bentonow.com for personalized support.

The Bento .NET SDK makes it quick and easy to send emails and track events in your .NET applications. We provide powerful and customizable APIs that can be used out-of-the-box to manage subscribers, track events, and send transactional emails. We also expose low-level APIs so that you can build fully custom experiences.

Get started with our [📚 integration guides](https://docs.bentonow.com), or [📘 browse the SDK reference](https://docs.bentonow.com/subscribers).

Table of Contents
=================

- [Bento .NET SDK](#bento-net-sdk)
- [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Requirements](#requirements)
  - [Getting started](#getting-started)
    - [Installation](#installation)
    - [Configuration](#configuration)
  - [Usage](#usage)
    - [Event Tracking](#event-tracking)
    - [Subscriber Management](#subscriber-management)
    - [Tag Management](#tag-management)
    - [Field Management](#field-management)
    - [Email Management](#email-management)
    - [Broadcast Management](#broadcast-management)
    - [Stats Management](#stats-management)
    - [Command Management](#command-management)
    - [Experimental Features](#experimental-features)
  - [Examples](#examples)
  - [Things to Know](#things-to-know)
  - [Contributing](#contributing)
  - [License](#license)

## Features

* **Event Tracking**: Easily track custom events and user behavior in your .NET application
* **Email Integration**: Send transactional emails via Bento
* **Subscriber Management**: Import and manage subscribers directly from your .NET app
* **API Access**: Full access to Bento's REST API for advanced operations
* **.NET-friendly**: Designed to work smoothly with .NET conventions and best practices

## Requirements

- .NET 8.0+
- Bento API Keys

## Getting started

[Previous content remains the same until Installation section]

### Installation

**Option 1: NuGet Package (Recommended)**

Install the SDK via NuGet Package Manager:

```bash
dotnet add package Bento.SDK
```

Or via Package Manager Console in Visual Studio:
```powershell
Install-Package Bento.SDK
```

**Option 2: Download DLL**

1. Download the latest release from [GitHub Releases](https://github.com/bentonow/bento-dotnet-sdk/releases)
2. Extract the ZIP file containing the SDK
3. Add a reference to the SDK in your project file (.csproj):

```xml
<ItemGroup>
    <Reference Include="Bento">
        <HintPath>path/to/extracted/zip/Bento.dll</HintPath>
    </Reference>
</ItemGroup>
```

Alternatively, you can add the reference through Visual Studio.


### Configuration

1. Add the Bento configuration section to your appsettings.json:

```json
{
  "Bento": {
    "PublishableKey": "bento-publishable-key",
    "SecretKey": "bento-secret-key",
    "SiteUuid": "bento-site-uuid"
  }
}
```

2. Register Bento services in your Program.cs or Startup.cs:

```csharp
using Bento.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Bento services
builder.Services.AddBentoClient(builder.Configuration);
```

## Usage

### Event Tracking

Track custom events in your application:

```csharp
using Bento.Models;
using Bento.Services;

public class ExampleService
{
    private readonly IBentoEventService _eventService;

    public ExampleService(IBentoEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task TrackEventExample()
    {
        var eventRequest = new EventRequest(
            Type: "completed_onboarding",
            Email: "user@example.com",
            Fields: new Dictionary<string, object>
            {
                { "first_name", "John" },
                { "last_name", "Doe" }
            },
            Details: new Dictionary<string, object>
            {
                { "source", "registration" }
            }
        );

        var response = await _eventService.TrackEventAsync<dynamic>(eventRequest);
    }
}
```

### Subscriber Management

Manage subscribers in your Bento account:

```csharp
public class SubscriberExample
{
    private readonly IBentoSubscriberService _subscriberService;

    public SubscriberExample(IBentoSubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    public async Task SubscriberExamples()
    {
        // Find subscriber
        var findResponse = await _subscriberService.FindSubscriberAsync<dynamic>("test@example.com");

        // Create subscriber
        var subscriberRequest = new SubscriberRequest(
            Email: "test@example.com",
            FirstName: "John",
            LastName: "Doe",
            Tags: new[] { "lead", "mql" }
        );
        var createResponse = await _subscriberService.CreateSubscriberAsync<dynamic>(subscriberRequest);

        // Import subscribers
        var subscribers = new List<SubscriberRequest>
        {
            new(
                Email: "user1@example.com",
                FirstName: "User",
                LastName: "One",
                Tags: new[] { "imported", "test" }
            ),
            new(
                Email: "user2@example.com",
                FirstName: "User",
                LastName: "Two",
                Tags: new[] { "imported", "test" }
            )
        };
        var importResponse = await _subscriberService.ImportSubscribersAsync<dynamic>(subscribers);
    }
}
```

### Tag Management

Manage tags in your Bento account:

```csharp
public class TagExample
{
    private readonly IBentoTagService _tagService;

    public TagExample(IBentoTagService tagService)
    {
        _tagService = tagService;
    }

    public async Task TagExamples()
    {
        // Get all tags
        var getTagsResponse = await _tagService.GetTagsAsync<dynamic>();

        // Create a new tag
        var tagRequest = new TagRequest("new_tag");
        var createTagResponse = await _tagService.CreateTagAsync<dynamic>(tagRequest);
    }
}
```

### Field Management

Manage custom fields in your Bento account:

```csharp
public class FieldExample
{
    private readonly IBentoFieldService _fieldService;

    public FieldExample(IBentoFieldService fieldService)
    {
        _fieldService = fieldService;
    }

    public async Task FieldExamples()
    {
        // Get all fields
        var getFieldsResponse = await _fieldService.GetFieldsAsync<dynamic>();

        // Create a new field
        var fieldRequest = new FieldRequest("custom_field");
        var createFieldResponse = await _fieldService.CreateFieldAsync<dynamic>(fieldRequest);
    }
}
```

### Email Management

Send transactional emails through Bento:

```csharp
public class EmailExample
{
    private readonly IBentoEmailService _emailService;

    public EmailExample(IBentoEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task EmailExamples()
    {
        var emailRequest = new EmailRequest(
            From: "sender@example.com",
            Subject: "Welcome to Our App",
            HtmlBody: "<p>Welcome aboard!</p>",
            To: "recipient@example.com"
        );

        var response = await _emailService.SendEmailAsync<dynamic>(emailRequest);
    }
}
```

### Broadcast Management

Manage email broadcasts:

```csharp
public class BroadcastExample
{
    private readonly IBentoBroadcastService _broadcastService;

    public BroadcastExample(IBentoBroadcastService broadcastService)
    {
        _broadcastService = broadcastService;
    }

    public async Task BroadcastExamples()
    {
        // Get all broadcasts
        var getBroadcastsResponse = await _broadcastService.GetBroadcastsAsync<dynamic>();

        // Create a new broadcast
        var broadcastRequest = new BroadcastRequest(
            Name: "Monthly Newsletter",
            Subject: "Your Monthly Update",
            Content: "<p>Hello {{ visitor.first_name }}</p>",
            Type: "plain",
            From: new ContactInfo(
                EmailAddress: "newsletter@example.com",
                Name: "Company Newsletter"
            ),
            InclusiveTags: "newsletter,active",
            BatchSizePerHour: 1000
        );

        var createResponse = await _broadcastService.CreateBroadcastAsync<dynamic>(broadcastRequest);
    }
}
```

### Stats Management

Get statistics from your Bento account:

```csharp
public class StatsExample
{
    private readonly IBentoStatsService _statsService;

    public StatsExample(IBentoStatsService statsService)
    {
        _statsService = statsService;
    }

    public async Task StatsExamples()
    {
        // Get site stats
        var siteStatsResponse = await _statsService.GetSiteStatsAsync<dynamic>();

        // Get segment stats
        var segmentStatsResponse = await _statsService.GetSegmentStatsAsync<dynamic>("segment_id");

        // Get report stats
        var reportStatsResponse = await _statsService.GetReportStatsAsync<dynamic>("report_id");
    }
}
```

### Command Management

Execute commands on subscribers:

```csharp
public class CommandExample
{
    private readonly IBentoCommandService _commandService;

    public CommandExample(IBentoCommandService commandService)
    {
        _commandService = commandService;
    }

    public async Task CommandExamples()
    {
        var commandRequest = new CommandRequest(
            Command: "add_tag",
            Email: "user@example.com",
            Query: "new_tag"
        );

        var response = await _commandService.ExecuteCommandAsync<dynamic>(commandRequest);
    }
}
```

### Experimental Features

Bento provides several experimental features:

```csharp
public class ExperimentalExample
{
    private readonly IBentoBlacklistService _blacklistService;
    private readonly IBentoValidationService _validationService;
    private readonly IBentoModerationService _moderationService;
    private readonly IBentoGenderService _genderService;
    private readonly IBentoGeolocationService _geolocationService;

    public ExperimentalExample(
        IBentoBlacklistService blacklistService,
        IBentoValidationService validationService,
        IBentoModerationService moderationService,
        IBentoGenderService genderService,
        IBentoGeolocationService geolocationService)
    {
        _blacklistService = blacklistService;
        _validationService = validationService;
        _moderationService = moderationService;
        _genderService = genderService;
        _geolocationService = geolocationService;
    }

    public async Task ExperimentalExamples()
    {
        // Check blacklist status
        var blacklistResponse = await _blacklistService.GetBlacklistStatusAsync<dynamic>(
            new BlacklistStatusRequest(Domain: "example.com")
        );

        // Validate email
        var validationResponse = await _validationService.ValidateEmailAsync<dynamic>(
            new EmailValidationRequest(EmailAddress: "test@example.com")
        );

        // Content moderation
        var moderationResponse = await _moderationService.ModerateContentAsync<dynamic>(
            new ContentModerationRequest("Content to moderate")
        );

        // Gender prediction
        var genderResponse = await _genderService.PredictGenderAsync<dynamic>(
            new GenderRequest("John Doe")
        );

        // Geolocation
        var geolocationResponse = await _geolocationService.GeolocateIpAsync<dynamic>(
            new GeolocationRequest("1.1.1.1")
        );
    }
}
```

## Examples

For complete working examples of all features, check out the `Bento.Examples` project in the solution. This project provides a comprehensive demonstration of all SDK capabilities and serves as a reference implementation.

To run the examples:

1. Clone the repository
2. Add your Bento credentials to `Bento.Examples/appsettings.json`
3. Run the example project:
```bash
cd Bento.Examples
dotnet run
```

## Things to Know

1. **Installation**: The SDK is available via NuGet for easy installation and updates
2. The SDK uses .NET's HttpClient factory pattern for optimal performance
3. All API requests return a `BentoResponse<T>` with standardized success/error handling
4. The SDK supports .NET's dependency injection for easy integration
5. All services are interface-based for easy testing and mocking
6. The SDK supports asynchronous operations throughout
7. For more advanced usage, refer to the [Bento API Documentation](https://docs.bentonow.com)

## Contributing

We welcome contributions! Please feel free to submit pull requests, report issues, and suggest improvements.

## License

The Bento SDK for .NET is available as open source under the terms of the MIT License.