using System.Text.Json.Serialization;

namespace MailerSend.Models.InboundRoutes;

/// <summary>
/// Inbound route configuration
/// </summary>
public class InboundRoute
{
    /// <summary>
    /// Gets or sets the inbound route ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the route name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the domain information
    /// </summary>
    [JsonPropertyName("domain")]
    public InboundRouteDomain? Domain { get; set; }

    /// <summary>
    /// Gets or sets when DNS was last checked
    /// </summary>
    [JsonPropertyName("dns_checked_at")]
    public DateTime? DnsCheckedAt { get; set; }

    /// <summary>
    /// Gets or sets whether the route is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the filters
    /// </summary>
    [JsonPropertyName("filters")]
    public Filter[] Filters { get; set; } = Array.Empty<Filter>();

    /// <summary>
    /// Gets or sets the forwards
    /// </summary>
    [JsonPropertyName("forwards")]
    public Forward[] Forwards { get; set; } = Array.Empty<Forward>();

    /// <summary>
    /// Gets or sets the MX values
    /// </summary>
    [JsonPropertyName("mx_values")]
    public MxValues? MxValues { get; set; }

    /// <summary>
    /// Gets or sets the priority
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update timestamp
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
