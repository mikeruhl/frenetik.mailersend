using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Tokens;

/// <summary>
/// Response containing a single token
/// </summary>
public class SingleTokenResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the token data
    /// </summary>
    [JsonPropertyName("data")]
    public Token Token { get; set; } = new Token();
}
