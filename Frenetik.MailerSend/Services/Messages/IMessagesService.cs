using Frenetik.MailerSend.Models.Messages;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.Messages;

/// <summary>
/// Service for retrieving messages
/// </summary>
public interface IMessagesService
{
    /// <summary>
    /// Gets a paginated list of messages
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<MessagesList> GetMessagesAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of messages (synchronous)
    /// </summary>
    MessagesList GetMessages(PaginationParameters? pagination = null);

    /// <summary>
    /// Gets a single message by ID
    /// </summary>
    /// <param name="messageId">Message ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Message> GetMessageAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single message by ID (synchronous)
    /// </summary>
    Message GetMessage(string messageId);
}
