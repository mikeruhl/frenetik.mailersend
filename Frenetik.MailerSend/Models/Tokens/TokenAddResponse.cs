using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Tokens;

/// <summary>
/// Response when creating a new token
/// </summary>
public class TokenAddResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the token data
    /// </summary>
    [JsonPropertyName("data")]
    public TokenAdd Token { get; set; } = new TokenAdd();
}
