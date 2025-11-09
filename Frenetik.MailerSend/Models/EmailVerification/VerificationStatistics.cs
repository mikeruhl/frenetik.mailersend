using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// Email verification statistics
/// </summary>
public class VerificationStatistics
{
    /// <summary>
    /// Gets or sets the valid count
    /// </summary>
    [JsonPropertyName("valid")]
    public int Valid { get; set; }

    /// <summary>
    /// Gets or sets the risky count
    /// </summary>
    [JsonPropertyName("risky")]
    public int Risky { get; set; }

    /// <summary>
    /// Gets or sets the unknown count
    /// </summary>
    [JsonPropertyName("unknown")]
    public int Unknown { get; set; }

    /// <summary>
    /// Gets or sets the duplicate count
    /// </summary>
    [JsonPropertyName("duplicate")]
    public int Duplicate { get; set; }

    /// <summary>
    /// Gets or sets the catchall count
    /// </summary>
    [JsonPropertyName("catchall")]
    public int Catchall { get; set; }

    /// <summary>
    /// Gets or sets the disposable count
    /// </summary>
    [JsonPropertyName("disposable")]
    public int Disposable { get; set; }

    /// <summary>
    /// Gets or sets the invalid count
    /// </summary>
    [JsonPropertyName("invalid")]
    public int Invalid { get; set; }
}
