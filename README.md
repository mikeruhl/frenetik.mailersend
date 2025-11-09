<div align="center">
  <img src="img/icon.png" alt="MailerSend .NET SDK" width="128"/>

  # MailerSend .NET SDK

  **A community-maintained .NET SDK for the MailerSend API**

  [![Build Status](https://github.com/mikeruhl/frenetik.mailerSend/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/mikeruhl/frenetik.mailerSend/actions/workflows/ci-cd.yml)
  [![NuGet Version](https://img.shields.io/nuget/v/Frenetik.MailerSend.svg?style=flat)](https://www.nuget.org/packages/Frenetik.MailerSend/)
  [![NuGet Downloads](https://img.shields.io/nuget/dt/Frenetik.MailerSend.svg?style=flat)](https://www.nuget.org/packages/Frenetik.MailerSend/)
  [![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE)
</div>

> **Note**: This is a community-maintained third-party SDK for the MailerSend API. It is not officially affiliated with or supported by MailerSend. For official MailerSend support, please visit [MailerSend Support](https://www.mailersend.com/help).

# Table of Contents
- [Installation](#installation)
- [Usage](#usage)
    - [Email](#email)
        - [Send an email](#send-an-email)
        - [Add CC, BCC recipients](#add-cc-bcc-recipients)
        - [Send a template-based email](#send-a-template-based-email)
        - [Personalization](#personalization)
        - [Send email with attachment](#send-email-with-attachment)
        - [Schedule an email](#schedule-an-email)
        - [Send bulk emails](#send-bulk-emails)
    - [Inbound routes](#inbound-routes)
        - [Get a list of inbound routes](#get-a-list-of-inbound-routes)
        - [Get an inbound route](#get-an-inbound-route)
        - [Create an inbound route](#create-an-inbound-route)
        - [Update an inbound route](#update-an-inbound-route)
        - [Delete an inbound route](#delete-an-inbound-route)
    - [Activities](#activities)
        - [Get a list of Activities](#get-a-list-of-activities)
    - [Analytics](#analytics)
        - [Activity data by date](#activity-data-by-date)
        - [Opens by country](#opens-by-country)
        - [Opens by user-agent name](#opens-by-user-agent-name)
        - [Opens by reading environment](#opens-by-reading-environment)
    - [Domains](#domains)
        - [Get a list of domains](#get-a-list-of-domains)
        - [Get a single domain](#get-a-single-domain)
        - [Delete a domain](#delete-a-domain)
        - [Add a Domain](#add-a-domain)
        - [Get DNS Records](#get-dns-records)
        - [Verify a Domain](#verify-a-domain)
        - [Get a list of recipients per domain](#get-a-list-of-recipients-per-domain)
        - [Update domain settings](#update-domain-settings)
    - [Messages](#messages)
        - [Get a list of messages](#get-a-list-of-messages)
        - [Get a single message](#get-a-single-message)
    - [Scheduled messages](#scheduled-messages)
        - [Get a list of scheduled messages](#get-a-list-of-scheduled-messages)
        - [Get a scheduled message](#get-a-scheduled-message)
        - [Delete a scheduled message](#delete-a-scheduled-message)
    - [Tokens](#tokens)
        - [Create a token](#create-a-token)
        - [Update token](#update-token)
        - [Delete token](#delete-token)
    - [Recipients](#recipients)
        - [Get recipients from a suppression list](#get-recipients-from-a-suppression-list)
        - [Add recipients to a suppression list](#add-recipients-to-a-suppression-list)
        - [Delete recipients from a suppression list](#delete-recipients-from-a-suppression-list)
    - [Webhooks](#webhooks)
        - [Get a list of webhooks](#get-a-list-of-webhooks)
        - [Get a single webhook](#get-a-single-webhook)
        - [Create a webhook](#create-a-webhook)
        - [Update a webhook](#update-a-webhook)
        - [Delete a webhook](#delete-a-webhook)
    - [Templates](#templates)
        - [Get a list of templates](#get-a-list-of-templates)
        - [Get a single template](#get-a-single-template)
        - [Delete a template](#delete-a-template)
    - [Email verification](#email-verification)
        - [Get all email verification lists](#get-all-email-verification-lists)
        - [Get an email verification list](#get-an-email-verification-list)
        - [Create an email verification list](#create-an-email-verification-list)
        - [Verify an email list](#verify-an-email-list)
        - [Get email verification list results](#get-email-verification-list-results)
    - [SMS](#sms)
        - [Send an SMS](#send-an-sms)

- [Testing](#testing)
- [Support and Feedback](#support-and-feedback)
- [License](#license)

<a name="installation"></a>

# Installation

Using .NET CLI:

```bash
dotnet add package Frenetik.MailerSend
```

Using Package Manager:

```
Install-Package Frenetik.MailerSend
```

# Usage

The SDK requires **Dependency Injection** with `IHttpClientFactory` for proper HttpClient lifecycle management. This ensures optimal connection pooling, DNS refresh handling, and follows .NET best practices.

## Setup (Dependency Injection Required)

For ASP.NET Core applications or any application using `Microsoft.Extensions.DependencyInjection`:

```csharp
using Frenetik.MailerSend.Configuration;

// In your Program.cs or Startup.cs
builder.Services.AddMailerSend(options =>
{
    options.ApiToken = "your_api_token";
    // Optional: customize timeout (default is 30 seconds)
    options.TimeoutSeconds = 60;
});

// Or simply with just the API token
builder.Services.AddMailerSend("your_api_token");

// Then inject MailerSendClient in your services/controllers
public class EmailController : ControllerBase
{
    private readonly MailerSendClient _mailerSend;

    public EmailController(MailerSendClient mailerSend)
    {
        _mailerSend = mailerSend;
    }

    public async Task<IActionResult> SendEmail()
    {
        var email = _mailerSend.Email.CreateEmail()
            .SetFrom("your@email.com", "Your Name")
            .AddRecipient("recipient@email.com", "Recipient Name")
            .SetSubject("Test Email")
            .SetHtmlContent("<p>Hello from DI!</p>");

        await _mailerSend.Email.SendAsync(email);
        return Ok();
    }
}
```

## Configuration via appsettings.json

The SDK uses the IOptions pattern, allowing you to configure MailerSend through `appsettings.json`:

**appsettings.json**:
```json
{
  "MailerSend": {
    "ApiToken": "your_api_token_here",
    "TimeoutSeconds": 60,
    "BaseUrl": "https://api.mailersend.com/v1"
  }
}
```

**Program.cs**:
```csharp
using Frenetik.MailerSend.Configuration;

// Bind configuration from appsettings.json
builder.Services.Configure<MailerSendOptions>(
    builder.Configuration.GetSection("MailerSend"));

// Register MailerSend services
builder.Services.AddMailerSend();
```

Or combine both approaches:

```csharp
// Load from appsettings.json and override specific values
builder.Services.AddMailerSend(options =>
{
    builder.Configuration.GetSection("MailerSend").Bind(options);
    // Override if needed
    options.TimeoutSeconds = 90;
});
```

## Environment Variable Fallback

If you don't provide an API token in the configuration, the SDK will automatically check for the `MAILERSEND_API_TOKEN` environment variable:

```csharp
// No API token in code - will use MAILERSEND_API_TOKEN environment variable
builder.Services.AddMailerSend(options => { });
```

## Console Applications

For console applications, you can still use dependency injection:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Frenetik.MailerSend;
using Frenetik.MailerSend.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMailerSend("your_api_token");
    })
    .Build();

var mailerSend = host.Services.GetRequiredService<MailerSendClient>();
var email = mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .SetSubject("Test Email")
    .SetHtmlContent("<p>Hello from Console App!</p>");

await mailerSend.Email.SendAsync(email);
```

## Polly Integration for Resilience

The SDK returns an `IHttpClientBuilder` from `AddMailerSend()`, making it easy to chain Polly resilience policies for retry logic, circuit breakers, and timeout handling:

```csharp
using Polly;
using Polly.Extensions.Http;

builder.Services.AddMailerSend(options =>
{
    options.ApiToken = "your_api_token";
})
.AddPolicyHandler(HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutRejectedException>()
    .WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
.AddPolicyHandler(HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
```

## Why IHttpClientFactory?

The SDK enforces `IHttpClientFactory` usage to ensure:

- **Proper connection pooling** - Avoids socket exhaustion
- **DNS refresh** - Handlers are rotated every 2 minutes to respect DNS changes
- **No lifecycle management** - The library doesn't control HttpClient disposal
- **Testability** - Easy to mock in unit tests
- **Extensibility** - Simple integration with Polly, custom handlers, or alternative HTTP implementations (like RestSharp)

## Email

The SDK provides a simple interface to send an email through MailerSend. Check the examples below for various use cases.

**Important**: All examples below assume you have configured MailerSend via dependency injection (see [Setup](#setup-dependency-injection-required)) and have injected `MailerSendClient` into your class.

The SDK returns a `MailerSendResponse` object on successful send or throws a `MailerSendException` on a failed one.

Through the `MailerSendResponse` object you can get the ID of the sent message, while the `MailerSendException` contains the response code and all errors that occurred. Check the respective `ResponseStatusCode` and `Errors` properties.

### Send an email

```csharp
using Frenetik.MailerSend;
using Frenetik.MailerSend.Exceptions;

public class EmailService
{
    private readonly MailerSendClient _mailerSend;

    // Injected via DI
    public EmailService(MailerSendClient mailerSend)
    {
        _mailerSend = mailerSend;
    }

    public async Task SendEmail()
    {
        var email = _mailerSend.Email.CreateEmail()
            .SetFrom("your@email.com", "Your Name")
            .AddRecipient("recipient@email.com", "Recipient Name")
            .SetSubject("Email subject")
            .SetPlainTextContent("This is the text content")
            .SetHtmlContent("<p>This is the HTML content</p>");

        try
        {
            var response = await _mailerSend.Email.SendAsync(email);
            Console.WriteLine($"Message ID: {response.MessageId}");
        }
        catch (MailerSendException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### Add CC, BCC recipients

```csharp
// Assumes _mailerSend is injected via DI
var email = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .AddCc("cc@email.com", "CC Name")
    .AddCc("cc2@email.com", "CC Name 2")
    .AddBcc("bcc@email.com", "BCC Name")
    .SetSubject("Email subject")
    .SetPlainTextContent("This is the text content")
    .SetHtmlContent("<p>This is the HTML content</p>");

var response = await _mailerSend.Email.SendAsync(email);
```

### Send a template-based email

```csharp
// Assumes _mailerSend is injected via DI
var email = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .SetTemplateId("Your MailerSend template ID");

var response = await _mailerSend.Email.SendAsync(email);
```

### Personalization

```csharp
// Assumes _mailerSend is injected via DI
var email = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .SetSubject("Subject {{ var }}")
    .SetPlainTextContent("This is the text version with a {{ var }}.")
    .SetHtmlContent("<p>This is the HTML version with a {{ var }}.</p>")
    .AddPersonalization("recipient@email.com", "var", "personalization value");

var response = await _mailerSend.Email.SendAsync(email);
```

### Send email with attachment

```csharp
// Assumes _mailerSend is injected via DI
var email = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .SetSubject("Email subject")
    .SetPlainTextContent("This is the text content")
    .SetHtmlContent("<p>This is the HTML content</p>")
    .AddAttachment("file.txt", @"C:\path\to\file.txt");

var response = await _mailerSend.Email.SendAsync(email);
```

### Schedule an email

```csharp
// Assumes _mailerSend is injected via DI
var sendAt = DateTime.UtcNow.AddHours(24);

var email = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient@email.com", "Recipient Name")
    .SetSubject("Email subject")
    .SetPlainTextContent("This is the text content")
    .SetHtmlContent("<p>This is the HTML content</p>")
    .SetSendAt(sendAt);

var response = await _mailerSend.Email.SendAsync(email);
```

### Send bulk emails

```csharp
// Assumes _mailerSend is injected via DI
var email1 = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient1@email.com", "Recipient 1")
    .SetSubject("Email subject 1")
    .SetPlainTextContent("This is the text content for the first email")
    .SetHtmlContent("<p>This is the HTML content for the first email</p>");

var email2 = _mailerSend.Email.CreateEmail()
    .SetFrom("your@email.com", "Your Name")
    .AddRecipient("recipient2@email.com", "Recipient 2")
    .SetSubject("Email subject 2")
    .SetPlainTextContent("This is the text content for the second email")
    .SetHtmlContent("<p>This is the HTML content for the second email</p>");

var bulkSendId = await _mailerSend.Email.BulkSendAsync(new[] { email1, email2 });
```

## Inbound routes

### Get a list of inbound routes

```csharp
// Assumes _mailerSend is injected via DI
var routes = await _mailerSend.InboundRoutes.GetRoutesAsync();

foreach (var route in routes.InboundRoutes)
{
    Console.WriteLine($"Route ID: {route.Id}");
    Console.WriteLine($"Route Name: {route.Name}");
}
```

### Get an inbound route

```csharp
// Assumes _mailerSend is injected via DI
var route = await _mailerSend.InboundRoutes.GetRouteAsync("inbound route id");
Console.WriteLine($"Route ID: {route.Id}");
Console.WriteLine($"Route Name: {route.Name}");
```

### Create an inbound route

```csharp
// Assumes _mailerSend is injected via DI
var route = await _mailerSend.InboundRoutes.CreateRouteAsync(
    domainId: "domain id",
    name: "Test inbound name",
    forwardUrl: "https://example-domain.com",
    matchFilter: "match_all",
    enabled: false
);

Console.WriteLine($"Route ID: {route.Id}");
```

### Update an inbound route

```csharp
// Assumes _mailerSend is injected via DI
var route = await _mailerSend.InboundRoutes.UpdateRouteAsync(
    routeId: "inbound route id",
    name: "Updated route name",
    enabled: true
);

Console.WriteLine($"Route ID: {route.Id}");
Console.WriteLine($"Route Name: {route.Name}");
```

### Delete an inbound route

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.InboundRoutes.DeleteRouteAsync("inbound route id");
Console.WriteLine($"Delete successful: {result}");
```

## Activities

### Get a list of activities

```csharp
// Assumes _mailerSend is injected via DI
var dateFrom = DateTime.UtcNow.AddDays(-30);
var dateTo = DateTime.UtcNow;
var events = new[] { "activity.sent", "activity.delivered" };

var activities = await _mailerSend.Activities.GetActivitiesAsync(
    dateFrom: dateFrom,
    dateTo: dateTo,
    events: events
);

foreach (var activity in activities.Activities)
{
    Console.WriteLine($"Activity ID: {activity.Id}");
    Console.WriteLine($"Type: {activity.Type}");
}
```

# Analytics

### Activity data by date

```csharp
// Assumes _mailerSend is injected via DI
var dateFrom = DateTime.UtcNow.AddDays(-30);
var dateTo = DateTime.UtcNow;
var events = new[] { "sent", "delivered", "opened" };

var analytics = await _mailerSend.Analytics.GetByDateAsync(
    dateFrom: dateFrom,
    dateTo: dateTo,
    events: events,
    domainId: "domain id"  // optional
);

foreach (var stat in analytics.Data)
{
    Console.WriteLine($"Date: {stat.StatDate}");
    Console.WriteLine($"Sent: {stat.Sent}");
    Console.WriteLine($"Delivered: {stat.Delivered}");
    Console.WriteLine($"Opened: {stat.Opened}");
}
```

### Opens by country

```csharp
// Assumes _mailerSend is injected via DI
var analytics = await _mailerSend.Analytics.GetOpensByCountryAsync();

foreach (var stat in analytics.Statistics)
{
    Console.WriteLine($"{stat.Name} - {stat.Count}");
}
```

### Opens by user agent name

```csharp
// Assumes _mailerSend is injected via DI
var analytics = await _mailerSend.Analytics.GetOpensByUserAgentAsync();

foreach (var stat in analytics.Statistics)
{
    Console.WriteLine($"{stat.Name} - {stat.Count}");
}
```

### Opens by reading environment

```csharp
// Assumes _mailerSend is injected via DI
var analytics = await _mailerSend.Analytics.GetOpensByUserAgentTypeAsync();

foreach (var stat in analytics.Statistics)
{
    Console.WriteLine($"{stat.Name} - {stat.Count}");
}
```

## Domains

### Get a list of domains

```csharp
// Assumes _mailerSend is injected via DI
var domains = await _mailerSend.Domains.GetDomainsAsync();

foreach (var domain in domains.Domains)
{
    Console.WriteLine($"Domain ID: {domain.Id}");
    Console.WriteLine($"Domain Name: {domain.Name}");
}
```

### Get a single domain

```csharp
// Assumes _mailerSend is injected via DI
var domain = await _mailerSend.Domains.GetDomainAsync("domain id");
Console.WriteLine($"Domain ID: {domain.Id}");
Console.WriteLine($"Domain Name: {domain.Name}");
```

### Delete a domain

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.Domains.DeleteDomainAsync("domain id");
Console.WriteLine($"Domain deleted: {result}");
```

### Add a domain

```csharp
// Assumes _mailerSend is injected via DI
var domain = await _mailerSend.Domains.CreateDomainAsync("example.com");
Console.WriteLine($"Domain ID: {domain.Id}");
Console.WriteLine($"Domain Name: {domain.Name}");
```

### Get DNS Records

```csharp
// Assumes _mailerSend is injected via DI
var records = await _mailerSend.Domains.GetDomainDnsRecordsAsync("domain id");

Console.WriteLine($"SPF: {records.Spf.Hostname} - {records.Spf.Value}");
Console.WriteLine($"DKIM: {records.Dkim.Hostname} - {records.Dkim.Value}");
```

### Verify a Domain

```csharp
// Assumes _mailerSend is injected via DI
var status = await _mailerSend.Domains.VerifyDomainAsync("domain id");
Console.WriteLine($"Verification message: {status.Message}");
```

### Get a list of recipients per domain

```csharp
// Assumes _mailerSend is injected via DI
var recipients = await _mailerSend.Domains.GetDomainRecipientsAsync("domain id");

foreach (var recipient in recipients.Recipients)
{
    Console.WriteLine($"Email: {recipient.Email}");
}
```

### Update domain settings

```csharp
// Assumes _mailerSend is injected via DI
var domain = await _mailerSend.Domains.UpdateDomainSettingsAsync(
    domainId: "domain id",
    customTrackingEnabled: true,
    sendPaused: false
);

Console.WriteLine($"Custom Tracking: {domain.DomainSettings.CustomTrackingEnabled}");
Console.WriteLine($"Send Paused: {domain.DomainSettings.SendPaused}");
```

## Messages

### Get a list of messages

```csharp
// Assumes _mailerSend is injected via DI
var messages = await _mailerSend.Messages.GetMessagesAsync();

foreach (var message in messages.Messages)
{
    Console.WriteLine($"Message ID: {message.Id}");
    Console.WriteLine($"Created: {message.CreatedAt}");
}
```

### Get a single message

```csharp
// Assumes _mailerSend is injected via DI
var message = await _mailerSend.Messages.GetMessageAsync("message id");
Console.WriteLine($"Message ID: {message.Id}");
Console.WriteLine($"Created: {message.CreatedAt}");
```

## Scheduled messages

### Get a list of scheduled messages

```csharp
// Assumes _mailerSend is injected via DI
var messages = await _mailerSend.ScheduledMessages.GetScheduledMessagesAsync();

foreach (var message in messages.ScheduledMessages)
{
    Console.WriteLine($"Message ID: {message.Id}");
    Console.WriteLine($"Subject: {message.Subject}");
}
```

### Get a scheduled message

```csharp
// Assumes _mailerSend is injected via DI
var message = await _mailerSend.ScheduledMessages.GetScheduledMessageAsync("message id");
Console.WriteLine($"Message ID: {message.Id}");
Console.WriteLine($"Subject: {message.Subject}");
```

### Delete a scheduled message

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.ScheduledMessages.DeleteScheduledMessageAsync("message id");
Console.WriteLine($"Delete successful: {result}");
```

## Tokens

### Create a token

```csharp
// Assumes _mailerSend is injected via DI
var scopes = new[] { "email_full", "domains_read" };

var token = await _mailerSend.Tokens.CreateTokenAsync(
    name: "Test token",
    domainId: "domain id",
    scopes: scopes
);

Console.WriteLine($"Token ID: {token.Id}");
Console.WriteLine($"Token Name: {token.Name}");
Console.WriteLine($"Access Token: {token.AccessToken}");
```

### Update token

```csharp
// Assumes _mailerSend is injected via DI
var token = await _mailerSend.Tokens.UpdateTokenStatusAsync(
    tokenId: "token id",
    status: "paused"
);

Console.WriteLine($"Token Name: {token.Name}");
Console.WriteLine($"Token Status: {token.Status}");
```

### Delete token

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.Tokens.DeleteTokenAsync("token id");
Console.WriteLine($"Delete successful: {result}");
```

## Recipients

### Get recipients from a suppression list

```csharp
// Assumes _mailerSend is injected via DI
var blocklist = await _mailerSend.Recipients.GetBlocklistAsync();

foreach (var suppression in blocklist.Suppressions)
{
    Console.WriteLine($"ID: {suppression.Id}");
    Console.WriteLine($"Recipient: {suppression.Recipient}");
}

var hardBounces = await _mailerSend.Recipients.GetHardBouncesAsync();

foreach (var suppression in hardBounces.Suppressions)
{
    Console.WriteLine($"ID: {suppression.Id}");
    Console.WriteLine($"Recipient: {suppression.Recipient}");
}

var spamComplaints = await _mailerSend.Recipients.GetSpamComplaintsAsync();

foreach (var suppression in spamComplaints.Suppressions)
{
    Console.WriteLine($"ID: {suppression.Id}");
    Console.WriteLine($"Recipient: {suppression.Recipient}");
}

var unsubscribes = await _mailerSend.Recipients.GetUnsubscribesAsync();

foreach (var suppression in unsubscribes.Suppressions)
{
    Console.WriteLine($"ID: {suppression.Id}");
    Console.WriteLine($"Recipient: {suppression.Recipient}");
}
```

### Add recipients to a suppression list

```csharp
// Assumes _mailerSend is injected via DI
var recipients = new[] { "test@example.com", "test2@example.com" };

var result = await _mailerSend.Recipients.AddToBlocklistAsync(recipients, "domain id");
Console.WriteLine($"Added to blocklist: {result}");
```

### Delete recipients from a suppression list

```csharp
// Assumes _mailerSend is injected via DI
var ids = new[] { "suppression id 1", "suppression id 2" };

var result = await _mailerSend.Recipients.DeleteFromBlocklistAsync(ids);
Console.WriteLine($"Deleted from blocklist: {result}");
```

## Webhooks

### Get a list of webhooks

```csharp
// Assumes _mailerSend is injected via DI
var webhooks = await _mailerSend.Webhooks.GetWebhooksAsync("domain id");

foreach (var webhook in webhooks.Webhooks)
{
    Console.WriteLine($"Webhook Name: {webhook.Name}");
}
```

### Get a single webhook

```csharp
// Assumes _mailerSend is injected via DI
var webhook = await _mailerSend.Webhooks.GetWebhookAsync("webhook id");
Console.WriteLine($"Webhook Name: {webhook.Name}");
```

### Create a webhook

```csharp
// Assumes _mailerSend is injected via DI
var events = new[] { "activity.sent", "activity.delivered" };

var webhook = await _mailerSend.Webhooks.CreateWebhookAsync(
    url: "https://example.com/webhook",
    name: "My Webhook",
    events: events,
    domainId: "domain id",
    enabled: true
);

Console.WriteLine($"Webhook Name: {webhook.Name}");
```

### Update a webhook

```csharp
// Assumes _mailerSend is injected via DI
var webhook = await _mailerSend.Webhooks.UpdateWebhookAsync(
    webhookId: "webhook id",
    name: "Updated Webhook Name",
    enabled: false
);

Console.WriteLine($"Webhook Name: {webhook.Name}");
```

### Delete a webhook

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.Webhooks.DeleteWebhookAsync("webhook id");
Console.WriteLine($"Delete successful: {result}");
```

## Templates

### Get a list of templates

```csharp
// Assumes _mailerSend is injected via DI
var templates = await _mailerSend.Templates.GetTemplatesAsync();

foreach (var template in templates.Templates)
{
    Console.WriteLine($"Template ID: {template.Id}");
    Console.WriteLine($"Template Name: {template.Name}");
}
```

### Get a single template

```csharp
// Assumes _mailerSend is injected via DI
var template = await _mailerSend.Templates.GetTemplateAsync("template id");
Console.WriteLine($"Template ID: {template.Id}");
Console.WriteLine($"Template Name: {template.Name}");
```

### Delete a template

```csharp
// Assumes _mailerSend is injected via DI
var result = await _mailerSend.Templates.DeleteTemplateAsync("template id");
Console.WriteLine($"Delete successful: {result}");
```

## Email verification

### Get all email verification lists

```csharp
// Assumes _mailerSend is injected via DI
var lists = await _mailerSend.EmailVerification.GetListsAsync();

foreach (var list in lists.Lists)
{
    Console.WriteLine($"List ID: {list.Id}");
    Console.WriteLine($"List Name: {list.Name}");
}
```

### Get an email verification list

```csharp
// Assumes _mailerSend is injected via DI
var list = await _mailerSend.EmailVerification.GetListAsync("list id");
Console.WriteLine($"List Name: {list.Name}");
```

### Create an email verification list

```csharp
// Assumes _mailerSend is injected via DI
var emails = new[] { "info@example.com", "info1@example.com", "info2@example.com" };

var list = await _mailerSend.EmailVerification.CreateListAsync(
    name: "Test email verification",
    emails: emails
);

Console.WriteLine($"List ID: {list.Id}");
```

### Verify an email list

```csharp
// Assumes _mailerSend is injected via DI
var list = await _mailerSend.EmailVerification.VerifyListAsync("list id");
Console.WriteLine($"Verification status: {list.Status?.Name}");
```

### Get email verification list results

```csharp
// Assumes _mailerSend is injected via DI
var results = await _mailerSend.EmailVerification.GetVerificationResultsAsync("list id");

foreach (var result in results.Results)
{
    Console.WriteLine($"Address: {result.Address}");
    Console.WriteLine($"Result: {result.Result}");
}
```

## SMS

### Send an SMS

```csharp
// Assumes _mailerSend is injected via DI
var messageId = await _mailerSend.Sms.SendSmsAsync(
    from: "from phone number",
    to: new[] { "to phone number" },
    text: "Test SMS message"
);

Console.WriteLine($"Message ID: {messageId}");
```

# Testing

Run tests using:
```bash
dotnet test
```

<a name="support-and-feedback"></a>
# Support and Feedback

This is a community-maintained third-party SDK. For SDK-related issues:

- **Bug Reports**: [Open an issue](https://github.com/mikeruhl/frenetik.mailerSend/issues/new?template=bug_report.md)
- **Feature Requests**: [Submit a feature request](https://github.com/mikeruhl/frenetik.mailerSend/issues/new?template=feature_request.md)
- **Questions**: [Ask a question](https://github.com/mikeruhl/frenetik.mailerSend/issues/new?template=question.md)
- **Contributing**: See [CONTRIBUTING.md](CONTRIBUTING.md)

For MailerSend API questions and official support, please refer to the [MailerSend API documentation](https://developers.mailersend.com)

<a name="license"></a>
# License

[The MIT License (MIT)](LICENSE)
