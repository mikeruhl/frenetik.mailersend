using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// Represents a domain in MailerSend
/// </summary>
public class Domain : MailerSendResponse
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
    /// Gets or sets whether DKIM is configured
    /// </summary>
    [JsonPropertyName("dkim")]
    public bool Dkim { get; set; }

    /// <summary>
    /// Gets or sets whether SPF is configured
    /// </summary>
    [JsonPropertyName("spf")]
    public bool Spf { get; set; }

    /// <summary>
    /// Gets or sets whether tracking is configured
    /// </summary>
    [JsonPropertyName("tracking")]
    public bool Tracking { get; set; }

    /// <summary>
    /// Gets or sets whether the domain is verified
    /// </summary>
    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets whether CNAME is verified
    /// </summary>
    [JsonPropertyName("is_cname_verified")]
    public bool IsCnameVerified { get; set; }

    /// <summary>
    /// Gets or sets whether DNS is active
    /// </summary>
    [JsonPropertyName("is_dns_active")]
    public bool IsDnsActive { get; set; }

    /// <summary>
    /// Gets or sets whether CNAME is active
    /// </summary>
    [JsonPropertyName("is_cname_active")]
    public bool IsCnameActive { get; set; }

    /// <summary>
    /// Gets or sets whether tracking is allowed
    /// </summary>
    [JsonPropertyName("is_tracking_allowed")]
    public bool IsTrackingAllowed { get; set; }

    /// <summary>
    /// Gets or sets whether there are queued messages
    /// </summary>
    [JsonPropertyName("has_not_queued_messaged")]
    public bool HasNotQueuedMessaged { get; set; }

    /// <summary>
    /// Gets or sets the count of not queued messages
    /// </summary>
    [JsonPropertyName("not_queued_messages_count")]
    public int NotQueuedMessagesCount { get; set; }

    /// <summary>
    /// Gets or sets the domain settings
    /// </summary>
    [JsonPropertyName("domain_settings")]
    public DomainSettings? DomainSettings { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
