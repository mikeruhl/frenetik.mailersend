using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Email;

/// <summary>
/// Represents a variable substitution
/// </summary>
public class Substitution
{
    /// <summary>
    /// Gets or sets the variable name
    /// </summary>
    [JsonPropertyName("var")]
    public string Variable { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Substitution"/> class
    /// </summary>
    public Substitution()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Substitution class
    /// </summary>
    /// <param name="variable">Variable name</param>
    /// <param name="value">Variable value</param>
    public Substitution(string variable, string value)
    {
        Variable = variable;
        Value = value;
    }
}
