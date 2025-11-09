using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Email;

/// <summary>
/// Represents variables for a specific recipient
/// </summary>
public class Variable
{
    /// <summary>
    /// Gets or sets the recipient's email address
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the substitutions
    /// </summary>
    [JsonPropertyName("substitutions")]
    public List<Substitution> Substitutions { get; set; } = new();

    /// <summary>
    /// Adds or replaces a substitution
    /// </summary>
    /// <param name="substitution">The substitution to add</param>
    public void AddSubstitution(Substitution substitution)
    {
        var existing = Substitutions.FirstOrDefault(s => s.Variable == substitution.Variable);
        if (existing != null)
        {
            existing.Value = substitution.Value;
        }
        else
        {
            Substitutions.Add(new Substitution(substitution.Variable, substitution.Value));
        }
    }
}
