using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// DNS record attribute
/// </summary>
public class DomainDnsAttribute
{
    /// <summary>
    /// Gets or sets the DNS record hostname
    /// </summary>
    [JsonPropertyName("hostname")]
    public string Hostname { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DNS record type (e.g., TXT, CNAME, MX)
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DNS record value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
