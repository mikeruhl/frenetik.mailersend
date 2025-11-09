using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Tokens;

/// <summary>
/// Paginated list of tokens
/// </summary>
public class TokensList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the tokens data
    /// </summary>
    [JsonPropertyName("data")]
    public Token[] Tokens { get; set; } = Array.Empty<Token>();
}
