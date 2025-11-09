using Frenetik.MailerSend.Models.EmailVerification;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.EmailVerification;

/// <summary>
/// Service for managing email verification
/// </summary>
public interface IEmailVerificationService
{
    /// <summary>
    /// Gets a paginated list of email verification lists
    /// </summary>
    Task<VerificationListsList> GetVerificationListsAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of email verification lists (synchronous)
    /// </summary>
    VerificationListsList GetVerificationLists(PaginationParameters? pagination = null);

    /// <summary>
    /// Gets a single email verification list by ID
    /// </summary>
    Task<VerificationList> GetVerificationListAsync(string listId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single email verification list by ID (synchronous)
    /// </summary>
    VerificationList GetVerificationList(string listId);

    /// <summary>
    /// Creates a new email verification list
    /// </summary>
    Task<VerificationList> CreateVerificationListAsync(string name, string[] emails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new email verification list (synchronous)
    /// </summary>
    VerificationList CreateVerificationList(string name, string[] emails);

    /// <summary>
    /// Verifies an email verification list
    /// </summary>
    Task<VerificationList> VerifyListAsync(string listId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies an email verification list (synchronous)
    /// </summary>
    VerificationList VerifyList(string listId);

    /// <summary>
    /// Gets verification results for a list
    /// </summary>
    Task<VerificationResultsList> GetVerificationResultsAsync(string listId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets verification results for a list (synchronous)
    /// </summary>
    VerificationResultsList GetVerificationResults(string listId);
}
