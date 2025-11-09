using System.Text.Json.Serialization;

namespace MailerSend.Models.Email;

/// <summary>
/// Represents an email recipient
/// </summary>
public class Recipient
{
    /// <summary>
    /// Gets or sets the recipient's name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the recipient's email address
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Recipient"/> class
    /// </summary>
    public Recipient()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Recipient class
    /// </summary>
    /// <param name="email">The email address</param>
    /// <param name="name">The recipient's name (optional)</param>
    public Recipient(string email, string? name = null)
    {
        Email = email;
        Name = name;
    }
}
