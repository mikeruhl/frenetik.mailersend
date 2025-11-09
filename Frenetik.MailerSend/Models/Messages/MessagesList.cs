using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Messages;

/// <summary>
/// Response containing a paginated list of messages
/// </summary>
public class MessagesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the array of messages
    /// </summary>
    [JsonPropertyName("data")]
    public MessagesListItem[] Messages { get; set; } = Array.Empty<MessagesListItem>();
}
