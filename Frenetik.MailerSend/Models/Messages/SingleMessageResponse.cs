using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Messages;

/// <summary>
/// Response containing a single message
/// </summary>
public class SingleMessageResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the message
    /// </summary>
    [JsonPropertyName("data")]
    public Message Message { get; set; } = new Message();
}
