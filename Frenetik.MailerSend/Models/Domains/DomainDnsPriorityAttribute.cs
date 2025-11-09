using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// DNS record attribute with priority
/// </summary>
public class DomainDnsPriorityAttribute : DomainDnsAttribute
{
    /// <summary>
    /// Gets or sets the DNS record priority
    /// </summary>
    [JsonPropertyName("priority")]
    public string Priority { get; set; } = string.Empty;
}
