using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Tokens;

/// <summary>
/// Response when creating a new token
/// </summary>
public class TokenAdd
{
    /// <summary>
    /// Gets or sets the token ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access token (only returned on creation)
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}
