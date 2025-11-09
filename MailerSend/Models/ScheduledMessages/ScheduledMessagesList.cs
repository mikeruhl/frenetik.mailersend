using System.Text.Json.Serialization;

namespace MailerSend.Models.ScheduledMessages;

/// <summary>
/// Paginated list of scheduled messages
/// </summary>
public class ScheduledMessagesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the scheduled messages data
    /// </summary>
    [JsonPropertyName("data")]
    public ScheduledMessage[] Messages { get; set; } = Array.Empty<ScheduledMessage>();
}
