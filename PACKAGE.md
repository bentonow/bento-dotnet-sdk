# Bento .NET SDK - Package Documentation

## What's Included

This NuGet package includes:

- Complete Bento .NET SDK for email marketing and automation
- Comprehensive README with usage examples
- Full API coverage for all Bento services
- Built-in dependency injection support
- Async/await pattern throughout
- Strong typing with nullable reference types

## Quick Setup

1. Install the package:
```bash
dotnet add package Bento.SDK
```

2. Configure in `appsettings.json`:
```json
{
  "Bento": {
    "PublishableKey": "your-publishable-key",
    "SecretKey": "your-secret-key", 
    "SiteUuid": "your-site-uuid"
  }
}
```

3. Register services in `Program.cs`:
```csharp
builder.Services.AddBentoClient(builder.Configuration);
```

## Key Features

- **Event Tracking**: Track user actions and behaviors
- **Subscriber Management**: Create, update, and manage subscribers
- **Email Management**: Send transactional emails
- **Tag & Field Management**: Organize subscriber data
- **Broadcasting**: Manage email campaigns
- **Analytics**: Access stats and reporting data
- **Experimental Features**: Email validation, content moderation, geolocation

## Complete Examples

The README included in this package contains comprehensive examples for all features. For additional working examples, visit the GitHub repository and check the `Bento.Examples` project.

## Support

- Documentation: https://docs.bentonow.com
- GitHub: https://github.com/bentonow/bento-dotnet-sdk
- Discord: https://discord.gg/ssXXFRmt5F
- Email: jesse@bentonow.com
