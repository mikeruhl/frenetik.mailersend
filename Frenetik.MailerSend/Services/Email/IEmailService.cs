using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Email;

namespace Frenetik.MailerSend.Services.Email;

/// <summary>
/// Service for sending and managing emails
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Gets or sets the default sender for emails
    /// </summary>
    Recipient? DefaultFrom { get; set; }

    /// <summary>
    /// Creates a new email message
    /// </summary>
    EmailMessage CreateEmail();

    /// <summary>
    /// Sends an email
    /// </summary>
    Task<MailerSendResponse> SendAsync(EmailMessage email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an email (synchronous)
    /// </summary>
    MailerSendResponse Send(EmailMessage email);

    /// <summary>
    /// Sends multiple emails in a bulk operation
    /// </summary>
    Task<string> BulkSendAsync(EmailMessage[] emails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends multiple emails in a bulk operation (synchronous)
    /// </summary>
    string BulkSend(EmailMessage[] emails);

    /// <summary>
    /// Gets the status of a bulk send operation
    /// </summary>
    Task<BulkSendStatus> GetBulkSendStatusAsync(string bulkSendId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the status of a bulk send operation (synchronous)
    /// </summary>
    BulkSendStatus GetBulkSendStatus(string bulkSendId);
}
