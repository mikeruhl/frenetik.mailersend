using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.InboundRoutes;

/// <summary>
/// Domain information for an inbound route
/// </summary>
public class InboundRouteDomain
{
    /// <summary>
    /// Gets or sets the domain ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the domain name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DKIM status
    /// </summary>
    [JsonPropertyName("dkim_status")]
    public string DkimStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tracking status
    /// </summary>
    [JsonPropertyName("tracking_status")]
    public string TrackingStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the domain is verified
    /// </summary>
    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}
