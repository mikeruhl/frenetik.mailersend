using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.ScheduledMessages;

/// <summary>
/// Response containing a single scheduled message
/// </summary>
public class SingleScheduledMessageResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the scheduled message data
    /// </summary>
    [JsonPropertyName("data")]
    public ScheduledMessage Message { get; set; } = new ScheduledMessage();
}
