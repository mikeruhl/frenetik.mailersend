using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// Domain verification status attributes
/// </summary>
public class DomainVerificationAttributes
{
    /// <summary>
    /// Gets or sets whether DKIM is verified
    /// </summary>
    [JsonPropertyName("dkim")]
    public bool Dkim { get; set; }

    /// <summary>
    /// Gets or sets whether SPF is verified
    /// </summary>
    [JsonPropertyName("spf")]
    public bool Spf { get; set; }

    /// <summary>
    /// Gets or sets whether MX is verified
    /// </summary>
    [JsonPropertyName("mx")]
    public bool Mx { get; set; }

    /// <summary>
    /// Gets or sets whether tracking is verified
    /// </summary>
    [JsonPropertyName("tracking")]
    public bool Tracking { get; set; }

    /// <summary>
    /// Gets or sets whether CNAME is verified
    /// </summary>
    [JsonPropertyName("cname")]
    public bool Cname { get; set; }

    /// <summary>
    /// Gets or sets whether return path CNAME is verified
    /// </summary>
    [JsonPropertyName("rp_cname")]
    public bool RpCname { get; set; }
}
