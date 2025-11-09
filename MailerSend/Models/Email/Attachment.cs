using System.Text.Json.Serialization;

namespace MailerSend.Models.Email;

/// <summary>
/// Represents an email attachment
/// </summary>
public class Attachment
{
    /// <summary>
    /// Gets or sets the base64 encoded content of the attachment
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filename of the attachment
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content ID (for inline images)
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the disposition (inline or attachment)
    /// </summary>
    [JsonPropertyName("disposition")]
    public string Disposition { get; set; } = "attachment";

    /// <summary>
    /// Sets the attachment from a file path
    /// </summary>
    /// <param name="filePath">Path to the file</param>
    public void AddAttachmentFromFile(string filePath)
    {
        var fileBytes = File.ReadAllBytes(filePath);
        Content = Convert.ToBase64String(fileBytes);
        Filename = Path.GetFileName(filePath);
        Disposition = "attachment";
    }

    /// <summary>
    /// Sets the attachment content and filename
    /// </summary>
    /// <param name="content">Base64 encoded content</param>
    /// <param name="filename">Filename</param>
    public void SetAttachment(string content, string filename)
    {
        Content = content;
        Filename = filename;
        Disposition = "attachment";
    }

    /// <summary>
    /// Sets an inline image from a file path
    /// </summary>
    /// <param name="contentId">Content ID for the image</param>
    /// <param name="filePath">Path to the image file</param>
    public void AddImageFromFile(string contentId, string filePath)
    {
        var fileBytes = File.ReadAllBytes(filePath);
        Id = contentId;
        Content = Convert.ToBase64String(fileBytes);
        Filename = Path.GetFileName(filePath);
        Disposition = !string.IsNullOrWhiteSpace(contentId) ? "inline" : "attachment";
    }

    /// <summary>
    /// Sets an inline image with content and filename
    /// </summary>
    /// <param name="contentId">Content ID for the image</param>
    /// <param name="content">Base64 encoded content</param>
    /// <param name="filename">Filename</param>
    public void SetImage(string contentId, string content, string filename)
    {
        Id = contentId;
        Content = content;
        Filename = filename;
        Disposition = !string.IsNullOrWhiteSpace(contentId) ? "inline" : "attachment";
    }
}
