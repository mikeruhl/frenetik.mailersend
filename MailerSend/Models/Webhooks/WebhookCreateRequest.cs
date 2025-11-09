using System.Text.Json.Serialization;

namespace MailerSend.Models.Webhooks;

/// <summary>
/// Request to create a webhook
/// </summary>
public class WebhookCreateRequest
{
    /// <summary>
    /// Gets or sets the webhook URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the webhook name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the webhook events
    /// </summary>
    [JsonPropertyName("events")]
    public string[] Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the domain ID
    /// </summary>
    [JsonPropertyName("domain_id")]
    public string DomainId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the webhook is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }
}
