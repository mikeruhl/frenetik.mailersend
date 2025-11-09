using System.Text.Json.Serialization;

namespace MailerSend.Models.Tokens;

/// <summary>
/// Request to create a new token
/// </summary>
public class TokenCreateRequest
{
    /// <summary>
    /// Gets or sets the token name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the domain ID
    /// </summary>
    [JsonPropertyName("domain_id")]
    public string DomainId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token scopes
    /// </summary>
    [JsonPropertyName("scopes")]
    public string[] Scopes { get; set; } = Array.Empty<string>();
}
