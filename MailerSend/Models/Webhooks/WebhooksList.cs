using System.Text.Json.Serialization;

namespace MailerSend.Models.Webhooks;

/// <summary>
/// List of webhooks
/// </summary>
public class WebhooksList : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the webhooks data
    /// </summary>
    [JsonPropertyName("data")]
    public Webhook[] Webhooks { get; set; } = Array.Empty<Webhook>();
}
