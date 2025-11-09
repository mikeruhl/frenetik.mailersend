using System.Text.Json.Serialization;

namespace MailerSend.Models.Domains;

/// <summary>
/// Response containing domain DNS records
/// </summary>
public class DomainDnsRecordsResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the DNS records
    /// </summary>
    [JsonPropertyName("data")]
    public DomainDnsRecords Records { get; set; } = new DomainDnsRecords();
}
