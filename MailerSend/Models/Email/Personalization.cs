using System.Text.Json.Serialization;

namespace MailerSend.Models.Email;

/// <summary>
/// Represents personalization data for a specific recipient
/// </summary>
public class Personalization
{
    /// <summary>
    /// Gets or sets the recipient's email address
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the personalization data
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object> Data { get; set; } = new();
}
