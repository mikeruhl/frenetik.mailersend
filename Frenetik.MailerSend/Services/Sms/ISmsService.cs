using Frenetik.MailerSend.Models.Sms;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.Sms;

/// <summary>
/// Service for managing SMS messages and phone numbers
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Gets a paginated list of SMS messages
    /// </summary>
    Task<SmsMessagesList> GetMessagesAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of SMS messages (synchronous)
    /// </summary>
    SmsMessagesList GetMessages(PaginationParameters? pagination = null);

    /// <summary>
    /// Gets a single SMS message by ID
    /// </summary>
    Task<SmsMessage> GetMessageAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single SMS message by ID (synchronous)
    /// </summary>
    SmsMessage GetMessage(string messageId);

    /// <summary>
    /// Sends an SMS message
    /// </summary>
    Task<string> SendSmsAsync(
        string from,
        string[] to,
        string text,
        List<SmsPersonalization>? personalization = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an SMS message (synchronous)
    /// </summary>
    string SendSms(string from, string[] to, string text, List<SmsPersonalization>? personalization = null);

    /// <summary>
    /// Gets a paginated list of phone numbers
    /// </summary>
    Task<PhoneNumberList> GetPhoneNumbersAsync(
        PaginationParameters? pagination = null,
        bool? paused = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of phone numbers (synchronous)
    /// </summary>
    PhoneNumberList GetPhoneNumbers(PaginationParameters? pagination = null, bool? paused = null);

    /// <summary>
    /// Gets a single phone number by ID
    /// </summary>
    Task<PhoneNumber> GetPhoneNumberAsync(string phoneNumberId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single phone number by ID (synchronous)
    /// </summary>
    PhoneNumber GetPhoneNumber(string phoneNumberId);

    /// <summary>
    /// Updates a phone number's paused status
    /// </summary>
    Task<PhoneNumber> UpdatePhoneNumberAsync(string phoneNumberId, bool paused, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a phone number's paused status (synchronous)
    /// </summary>
    PhoneNumber UpdatePhoneNumber(string phoneNumberId, bool paused);

    /// <summary>
    /// Deletes a phone number
    /// </summary>
    Task<bool> DeletePhoneNumberAsync(string phoneNumberId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a phone number (synchronous)
    /// </summary>
    bool DeletePhoneNumber(string phoneNumberId);
}
