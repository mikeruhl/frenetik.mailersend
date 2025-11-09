using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.InboundRoutes;

/// <summary>
/// Inbound route forward configuration
/// </summary>
public class Forward
{
    /// <summary>
    /// Gets or sets the forward type
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the forward value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret
    /// </summary>
    [JsonPropertyName("secret")]
    public string? Secret { get; set; }
}
