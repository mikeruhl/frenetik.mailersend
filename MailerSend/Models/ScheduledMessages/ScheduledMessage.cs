using System.Text.Json.Serialization;

namespace MailerSend.Models.ScheduledMessages;

/// <summary>
/// Represents a scheduled message
/// </summary>
public class ScheduledMessage
{
    /// <summary>
    /// Gets or sets the message ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the subject
    /// </summary>
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the send at date
    /// </summary>
    [JsonPropertyName("send_at")]
    public DateTime? SendAt { get; set; }

    /// <summary>
    /// Gets or sets the status
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status message
    /// </summary>
    [JsonPropertyName("status_message")]
    public string? StatusMessage { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}
