using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Webhooks;

/// <summary>
/// Response containing a single webhook
/// </summary>
public class SingleWebhookResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the webhook data
    /// </summary>
    [JsonPropertyName("data")]
    public Webhook Webhook { get; set; } = new Webhook();
}
