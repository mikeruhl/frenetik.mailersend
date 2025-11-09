using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Email;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.Email;

/// <summary>
/// Service implementation for sending and managing emails
/// </summary>
public class EmailService : ServiceBase, IEmailService
{
    /// <summary>
    /// Gets or sets the default sender for emails
    /// </summary>
    public Recipient? DefaultFrom { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public EmailService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Creates a new email message
    /// </summary>
    public EmailMessage CreateEmail()
    {
        var email = new EmailMessage();
        if (DefaultFrom != null)
        {
            email.From = DefaultFrom;
        }
        return email;
    }

    /// <summary>
    /// Sends an email
    /// </summary>
    public async Task<MailerSendResponse> SendAsync(EmailMessage email, CancellationToken cancellationToken = default)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        email.PrepareForSending();

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.PostRequestAsync<MailerSendResponse>("email", email, cancellationToken);
    }

    /// <summary>
    /// Sends an email (synchronous)
    /// </summary>
    public MailerSendResponse Send(EmailMessage email)
    {
        return SendAsync(email).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Sends multiple emails in a bulk operation
    /// </summary>
    public async Task<string> BulkSendAsync(EmailMessage[] emails, CancellationToken cancellationToken = default)
    {
        if (emails == null || emails.Length == 0)
            throw new ArgumentException("At least one email must be provided", nameof(emails));

        foreach (var email in emails)
        {
            email.PrepareForSending();
        }

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.PostRequestAsync<SendBulkResponse>("bulk-email", emails, cancellationToken);
        return response.BulkSendId ?? string.Empty;
    }

    /// <summary>
    /// Sends multiple emails in a bulk operation (synchronous)
    /// </summary>
    public string BulkSend(EmailMessage[] emails)
    {
        return BulkSendAsync(emails).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets the status of a bulk send operation
    /// </summary>
    public async Task<BulkSendStatus> GetBulkSendStatusAsync(string bulkSendId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(bulkSendId))
            throw new ArgumentException("Bulk send ID must be provided", nameof(bulkSendId));

        var endpoint = $"bulk-email/{bulkSendId}";

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.GetRequestAsync<BulkSendStatusResponse>(endpoint, cancellationToken);
        return response.Data ?? new BulkSendStatus();
    }

    /// <summary>
    /// Gets the status of a bulk send operation (synchronous)
    /// </summary>
    public BulkSendStatus GetBulkSendStatus(string bulkSendId)
    {
        return GetBulkSendStatusAsync(bulkSendId).GetAwaiter().GetResult();
    }

    private class BulkSendStatusResponse : MailerSendResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public BulkSendStatus? Data { get; set; }
    }
}
