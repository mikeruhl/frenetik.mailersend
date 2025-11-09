using Frenetik.MailerSend.Models.Webhooks;

namespace Frenetik.MailerSend.Services.Webhooks;

/// <summary>
/// Service for managing webhooks
/// </summary>
public interface IWebhooksService
{
    /// <summary>
    /// Gets all webhooks for a domain
    /// </summary>
    Task<WebhooksList> GetWebhooksAsync(
        string domainId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all webhooks for a domain (synchronous)
    /// </summary>
    WebhooksList GetWebhooks(string domainId);

    /// <summary>
    /// Gets a single webhook by ID
    /// </summary>
    Task<Webhook> GetWebhookAsync(
        string webhookId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single webhook by ID (synchronous)
    /// </summary>
    Webhook GetWebhook(string webhookId);

    /// <summary>
    /// Creates a new webhook
    /// </summary>
    Task<Webhook> CreateWebhookAsync(
        string url,
        string name,
        string[] events,
        string domainId,
        bool? enabled = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new webhook (synchronous)
    /// </summary>
    Webhook CreateWebhook(
        string url,
        string name,
        string[] events,
        string domainId,
        bool? enabled = null);

    /// <summary>
    /// Updates a webhook
    /// </summary>
    Task<Webhook> UpdateWebhookAsync(
        string webhookId,
        string? url = null,
        string? name = null,
        string[]? events = null,
        bool? enabled = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a webhook (synchronous)
    /// </summary>
    Webhook UpdateWebhook(
        string webhookId,
        string? url = null,
        string? name = null,
        string[]? events = null,
        bool? enabled = null);

    /// <summary>
    /// Deletes a webhook
    /// </summary>
    Task<bool> DeleteWebhookAsync(
        string webhookId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a webhook (synchronous)
    /// </summary>
    bool DeleteWebhook(string webhookId);
}
