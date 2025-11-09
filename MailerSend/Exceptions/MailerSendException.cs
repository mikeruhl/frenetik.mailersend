namespace MailerSend.Exceptions;

/// <summary>
/// Exception thrown when an error is returned from the MailerSend API
/// </summary>
public class MailerSendException : Exception
{
    /// <summary>
    /// Gets the HTTP response status code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Gets the raw response body from the API
    /// </summary>
    public string? ResponseBody { get; set; }

    /// <summary>
    /// Gets the validation errors returned from the API
    /// </summary>
    public Dictionary<string, string[]> Errors { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the MailerSendException class with a specified error message
    /// </summary>
    /// <param name="message">The error message</param>
    public MailerSendException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the MailerSendException class with a specified error message and inner exception
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="innerException">The inner exception</param>
    public MailerSendException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
