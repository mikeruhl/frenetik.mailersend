using System.Text.Json.Serialization;

namespace MailerSend.Models.Activities;

/// <summary>
/// Represents a single activity event
/// </summary>
public class Activity
{
    /// <summary>
    /// Gets or sets the activity ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the activity type (e.g., sent, delivered, opened, clicked)
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update timestamp
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the associated email information
    /// </summary>
    [JsonPropertyName("email")]
    public ActivityEmail? Email { get; set; }
}
