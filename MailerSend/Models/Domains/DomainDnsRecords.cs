using System.Text.Json.Serialization;

namespace MailerSend.Models.Domains;

/// <summary>
/// DNS records for a domain
/// </summary>
public class DomainDnsRecords
{
    /// <summary>
    /// Gets or sets the domain ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SPF DNS record
    /// </summary>
    [JsonPropertyName("spf")]
    public DomainDnsAttribute? Spf { get; set; }

    /// <summary>
    /// Gets or sets the DKIM DNS record
    /// </summary>
    [JsonPropertyName("dkim")]
    public DomainDnsAttribute? Dkim { get; set; }

    /// <summary>
    /// Gets or sets the return path DNS record
    /// </summary>
    [JsonPropertyName("return_path")]
    public DomainDnsAttribute? ReturnPath { get; set; }

    /// <summary>
    /// Gets or sets the custom tracking DNS record
    /// </summary>
    [JsonPropertyName("custom_tracking")]
    public DomainDnsAttribute? CustomTracking { get; set; }

    /// <summary>
    /// Gets or sets the inbound routing DNS record
    /// </summary>
    [JsonPropertyName("inbound_routing")]
    public DomainDnsPriorityAttribute? InboundRouting { get; set; }
}
