using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.InboundRoutes;

/// <summary>
/// List of inbound routes
/// </summary>
public class InboundRoutesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the inbound routes data
    /// </summary>
    [JsonPropertyName("data")]
    public InboundRoute[] Routes { get; set; } = Array.Empty<InboundRoute>();
}
