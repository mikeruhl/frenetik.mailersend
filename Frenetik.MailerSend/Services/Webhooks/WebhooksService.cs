using System.Text.Json;
using System.Text.Json.Serialization;
using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Webhooks;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.Webhooks;

/// <summary>
/// Service implementation for managing webhooks
/// </summary>
public class WebhooksService : ServiceBase, IWebhooksService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebhooksService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public WebhooksService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets all webhooks for a domain
    /// </summary>
    public async Task<WebhooksList> GetWebhooksAsync(
        string domainId,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"webhooks?domain_id={domainId}";
        return await mailerSendHttpClient.GetRequestAsync<WebhooksList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets all webhooks for a domain (synchronous)
    /// </summary>
    public WebhooksList GetWebhooks(string domainId)
    {
        return GetWebhooksAsync(domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single webhook by ID
    /// </summary>
    public async Task<Webhook> GetWebhookAsync(
        string webhookId,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"webhooks/{webhookId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleWebhookResponse>(endpoint, cancellationToken);
        return response.Webhook;
    }

    /// <summary>
    /// Gets a single webhook by ID (synchronous)
    /// </summary>
    public Webhook GetWebhook(string webhookId)
    {
        return GetWebhookAsync(webhookId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Creates a new webhook
    /// </summary>
    public async Task<Webhook> CreateWebhookAsync(
        string url,
        string name,
        string[] events,
        string domainId,
        bool? enabled = null,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new WebhookCreateRequest
        {
            Url = url,
            Name = name,
            Events = events,
            DomainId = domainId,
            Enabled = enabled
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = "webhooks";
        var response = await mailerSendHttpClient.PostRequestAsync<SingleWebhookResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.Webhook;
    }

    /// <summary>
    /// Creates a new webhook (synchronous)
    /// </summary>
    public Webhook CreateWebhook(
        string url,
        string name,
        string[] events,
        string domainId,
        bool? enabled = null)
    {
        return CreateWebhookAsync(url, name, events, domainId, enabled).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Updates a webhook
    /// </summary>
    public async Task<Webhook> UpdateWebhookAsync(
        string webhookId,
        string? url = null,
        string? name = null,
        string[]? events = null,
        bool? enabled = null,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new WebhookUpdateRequest
        {
            Url = url,
            Name = name,
            Events = events,
            Enabled = enabled
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = $"webhooks/{webhookId}";
        var response = await mailerSendHttpClient.PutRequestAsync<SingleWebhookResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.Webhook;
    }

    /// <summary>
    /// Updates a webhook (synchronous)
    /// </summary>
    public Webhook UpdateWebhook(
        string webhookId,
        string? url = null,
        string? name = null,
        string[]? events = null,
        bool? enabled = null)
    {
        return UpdateWebhookAsync(webhookId, url, name, events, enabled).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a webhook
    /// </summary>
    public async Task<bool> DeleteWebhookAsync(
        string webhookId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var mailerSendHttpClient = CreateHttpClient();

            var endpoint = $"webhooks/{webhookId}";
            var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(endpoint, cancellationToken);
            return IsSuccessStatusCode(response.ResponseStatusCode);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a webhook (synchronous)
    /// </summary>
    public bool DeleteWebhook(string webhookId)
    {
        return DeleteWebhookAsync(webhookId).GetAwaiter().GetResult();
    }
}
