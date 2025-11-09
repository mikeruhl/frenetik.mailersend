using System.Text.Json.Serialization;

namespace MailerSend.Models.Domains;

/// <summary>
/// Domain settings configuration
/// </summary>
public class DomainSettings
{
    /// <summary>
    /// Gets or sets whether sending is paused for this domain
    /// </summary>
    [JsonPropertyName("send_paused")]
    public bool? SendPaused { get; set; }

    /// <summary>
    /// Gets or sets whether click tracking is enabled
    /// </summary>
    [JsonPropertyName("track_clicks")]
    public bool? TrackClicks { get; set; }

    /// <summary>
    /// Gets or sets whether open tracking is enabled
    /// </summary>
    [JsonPropertyName("track_opens")]
    public bool? TrackOpens { get; set; }

    /// <summary>
    /// Gets or sets whether unsubscribe tracking is enabled
    /// </summary>
    [JsonPropertyName("track_unsubscribe")]
    public bool? TrackUnsubscribe { get; set; }

    /// <summary>
    /// Gets or sets the HTML content for unsubscribe tracking
    /// </summary>
    [JsonPropertyName("track_unsubscribe_html")]
    public string? TrackUnsubscribeHtml { get; set; }

    /// <summary>
    /// Gets or sets the plain text content for unsubscribe tracking
    /// </summary>
    [JsonPropertyName("track_unsubscribe_plain")]
    public string? TrackUnsubscribePlain { get; set; }

    /// <summary>
    /// Gets or sets whether content tracking is enabled
    /// </summary>
    [JsonPropertyName("track_content")]
    public bool? TrackContent { get; set; }

    /// <summary>
    /// Gets or sets whether custom tracking is enabled
    /// </summary>
    [JsonPropertyName("custom_tracking_enabled")]
    public bool? CustomTrackingEnabled { get; set; }

    /// <summary>
    /// Gets or sets the custom tracking subdomain
    /// </summary>
    [JsonPropertyName("custom_tracking_subdomain")]
    public string? CustomTrackingSubdomain { get; set; }

    /// <summary>
    /// Gets or sets the return path subdomain
    /// </summary>
    [JsonPropertyName("return_path_subdomain")]
    public string? ReturnPathSubdomain { get; set; }

    /// <summary>
    /// Gets or sets whether inbound routing is enabled
    /// </summary>
    [JsonPropertyName("inbound_routing_enabled")]
    public bool? InboundRoutingEnabled { get; set; }

    /// <summary>
    /// Gets or sets the inbound routing subdomain
    /// </summary>
    [JsonPropertyName("inbound_routing_subdomain")]
    public string? InboundRoutingSubdomain { get; set; }

    /// <summary>
    /// Gets or sets whether precedence bulk is enabled
    /// </summary>
    [JsonPropertyName("precedence_bulk")]
    public bool? PrecedenceBulk { get; set; }

    /// <summary>
    /// Gets or sets whether duplicated recipients should be ignored
    /// </summary>
    [JsonPropertyName("ignore_duplicated_recipients")]
    public bool? IgnoreDuplicatedRecipients { get; set; }
}
