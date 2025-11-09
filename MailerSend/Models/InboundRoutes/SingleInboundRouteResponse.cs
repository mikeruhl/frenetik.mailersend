using System.Text.Json.Serialization;

namespace MailerSend.Models.InboundRoutes;

/// <summary>
/// Response containing a single inbound route
/// </summary>
public class SingleInboundRouteResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the inbound route data
    /// </summary>
    [JsonPropertyName("data")]
    public InboundRoute Route { get; set; } = new InboundRoute();
}
