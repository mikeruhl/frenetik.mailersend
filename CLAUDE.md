# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the official MailerSend .NET SDK, a client library for the MailerSend API that supports sending transactional emails, managing domains, webhooks, templates, and SMS messages.

## Build and Test Commands

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests for a specific test class
dotnet test --filter "FullyQualifiedName~ClassName"

# Build in Release mode
dotnet build -c Release

# Pack the NuGet package
dotnet pack -c Release
```

## Architecture

### Multi-Target Framework
The main `MailerSend` project targets both `net8.0` and `netstandard2.0` for broad compatibility. Conditional compilation (`#if NETSTANDARD2_0`) is used where necessary (e.g., `ReadAsStringAsync` in `MailerSendHttpClient.cs:184-188`).

### Client Structure
The SDK follows a service-oriented architecture with IHttpClientFactory:

- **MailerSendClient**: Main entry point that requires `IHttpClientFactory` and `MailerSendOptions` in constructor. Provides access to all service modules.
- **MailerSendHttpClient**: Core HTTP client wrapper created per-request. Handles authentication, serialization, error handling, and rate limiting.
- **Service modules**: Each API domain (Email, Domains, Analytics, etc.) receives `IHttpClientFactory` and `MailerSendOptions` in constructor. Creates fresh `MailerSendHttpClient` for each HTTP operation.

All services are initialized in the `MailerSendClient` constructor and exposed as properties. Services use `IHttpClientFactory.CreateClient("MailerSend")` to get an HttpClient for each request.

### Configuration
API authentication uses Bearer token authentication via `MailerSendOptions`. The library requires dependency injection setup using `AddMailerSend()`:

```csharp
builder.Services.AddMailerSend(options => options.ApiToken = "your_api_token");
```

The API token can be provided:
1. In `AddMailerSend` configuration
2. Via `MAILERSEND_API_TOKEN` environment variable (fallback)

### Error Handling
All API errors throw `MailerSendException` which contains:
- `Message`: Error description
- `Code`: HTTP status code
- `Errors`: Dictionary of validation errors from API
- `ResponseBody`: Raw response for debugging

Successful responses return `MailerSendResponse` (or subclass) containing:
- `MessageId`: For email operations
- `ResponseStatusCode`: HTTP status
- `RateLimit` and `RateLimitRemaining`: Rate limit information
- `Headers`: All response headers

### Service Pattern
Each service follows this pattern:
- Interface (e.g., `IEmailService`) defines the contract
- Implementation (e.g., `EmailService`) uses `MailerSendHttpClient`
- Models are in `MailerSend.Models.*` organized by service

### Email Builder Pattern
The `EmailService` uses a fluent builder pattern:
```csharp
var email = client.Email.CreateEmail()
    .SetFrom("from@example.com")
    .AddRecipient("to@example.com")
    .SetSubject("Subject")
    .SetHtmlContent("<p>Content</p>");
```

## Project Structure

- `MailerSend/` - Main SDK library
  - `Configuration/` - Client configuration options
  - `Exceptions/` - Custom exception types
  - `Http/` - HTTP client wrapper and interface
  - `Models/` - Request/response models organized by service area
  - `Services/` - Service interfaces and implementations
  - `MailerSendClient.cs` - Main client entry point

- `MailerSend.Tests/` - xUnit test project using Moq for mocking

## Testing Framework

Tests use:
- xUnit as the test runner
- `HttpMessageHandler` mocking for HTTP responses
- Moq for `IHttpClientFactory` mocking
- Coverlet for code coverage

**Current Status**: Test infrastructure has been updated to use `HttpMessageHandler` mocking to support the new IHttpClientFactory pattern. The `MockHttpMessageHandler` helper class is available in `MailerSend.Tests/Helpers/`. Tests need JSON response format adjustments to match SDK expectations.

**Test Pattern**:
```csharp
private ServiceName CreateService(HttpMessageHandler handler)
{
    var httpClient = new HttpClient(handler)
    {
        BaseAddress = new Uri("https://api.mailersend.com/v1")
    };
    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer test_token");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

    var mockFactory = new Mock<IHttpClientFactory>();
    mockFactory.Setup(x => x.CreateClient("MailerSend")).Returns(httpClient);

    var options = new MailerSendOptions
    {
        ApiToken = "test_token",
        BaseUrl = "https://api.mailersend.com/v1"
    };

    return new ServiceName(mockFactory.Object, options);
}
```

## Development Notes

- Use XML documentation comments for all public APIs (enforced via `<GenerateDocumentationFile>true</GenerateDocumentationFile>`)
- Follow async/await patterns with `ConfigureAwait(false)` for library code
- All services must implement disposal pattern for proper resource cleanup
- JSON serialization uses `System.Text.Json` with property name case insensitivity
