using System.Text.Json.Serialization;

namespace MailerSend.Models.InboundRoutes;

/// <summary>
/// Inbound route filter
/// </summary>
public class Filter
{
    /// <summary>
    /// Gets or sets the filter type
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filter key
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filter value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparer
    /// </summary>
    [JsonPropertyName("comparer")]
    public string Comparer { get; set; } = string.Empty;
}
