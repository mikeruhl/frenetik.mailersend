using System.Text.Json.Serialization;

namespace MailerSend.Models.Tokens;

/// <summary>
/// Request to update a token's status or name
/// </summary>
public class TokenUpdateRequest
{
    /// <summary>
    /// Gets or sets the token status (pause/unpause)
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the token name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
