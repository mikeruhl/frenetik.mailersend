using Frenetik.MailerSend.Models.ScheduledMessages;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.ScheduledMessages;

/// <summary>
/// Service for managing scheduled messages
/// </summary>
public interface IScheduledMessagesService
{
    /// <summary>
    /// Gets a paginated list of scheduled messages
    /// </summary>
    Task<ScheduledMessagesList> GetScheduledMessagesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of scheduled messages (synchronous)
    /// </summary>
    ScheduledMessagesList GetScheduledMessages(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Gets a single scheduled message by ID
    /// </summary>
    Task<ScheduledMessage> GetScheduledMessageAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single scheduled message by ID (synchronous)
    /// </summary>
    ScheduledMessage GetScheduledMessage(string messageId);

    /// <summary>
    /// Deletes a scheduled message
    /// </summary>
    Task<bool> DeleteScheduledMessageAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a scheduled message (synchronous)
    /// </summary>
    bool DeleteScheduledMessage(string messageId);
}
