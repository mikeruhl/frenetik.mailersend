using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.InboundRoutes;

/// <summary>
/// MX record priority values
/// </summary>
public class MxValues
{
    /// <summary>
    /// Gets or sets the priority 1 value
    /// </summary>
    [JsonPropertyName("priority_1")]
    public string Priority1 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority 2 value
    /// </summary>
    [JsonPropertyName("priority_2")]
    public string Priority2 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority 3 value
    /// </summary>
    [JsonPropertyName("priority_3")]
    public string Priority3 { get; set; } = string.Empty;
}
