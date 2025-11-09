using System.Text.Json.Serialization;

namespace MailerSend.Models.Messages;

/// <summary>
/// Represents a message item in a paginated list
/// </summary>
public class MessagesListItem
{
    /// <summary>
    /// Gets or sets the message ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
