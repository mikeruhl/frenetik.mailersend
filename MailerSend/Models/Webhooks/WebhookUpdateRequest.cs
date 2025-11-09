using System.Text.Json.Serialization;

namespace MailerSend.Models.Webhooks;

/// <summary>
/// Request to update a webhook
/// </summary>
public class WebhookUpdateRequest
{
    /// <summary>
    /// Gets or sets the webhook URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the webhook name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the webhook events
    /// </summary>
    [JsonPropertyName("events")]
    public string[]? Events { get; set; }

    /// <summary>
    /// Gets or sets whether the webhook is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }
}
