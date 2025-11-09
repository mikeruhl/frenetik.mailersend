using MailerSend.Models.Recipients;
using MailerSend.Models.Util;

namespace MailerSend.Services.Recipients;

/// <summary>
/// Service for managing recipient suppression lists
/// </summary>
public interface IRecipientsService
{
    /// <summary>
    /// Gets blocklist recipients
    /// </summary>
    Task<SuppressionsList> GetBlocklistAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets blocklist recipients (synchronous)
    /// </summary>
    SuppressionsList GetBlocklist(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Adds recipients to blocklist
    /// </summary>
    Task<bool> AddToBlocklistAsync(
        string[] recipients,
        string? domainId = null,
        string[]? patterns = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds recipients to blocklist (synchronous)
    /// </summary>
    bool AddToBlocklist(string[] recipients, string? domainId = null, string[]? patterns = null);

    /// <summary>
    /// Deletes recipients from blocklist
    /// </summary>
    Task<bool> DeleteFromBlocklistAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes recipients from blocklist (synchronous)
    /// </summary>
    bool DeleteFromBlocklist(string[]? ids = null, bool deleteAll = false, string? domainId = null);

    /// <summary>
    /// Gets hard bounce recipients
    /// </summary>
    Task<SuppressionsList> GetHardBouncesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets hard bounce recipients (synchronous)
    /// </summary>
    SuppressionsList GetHardBounces(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Adds recipients to hard bounces list
    /// </summary>
    Task<bool> AddToHardBouncesAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds recipients to hard bounces list (synchronous)
    /// </summary>
    bool AddToHardBounces(string[] recipients, string? domainId = null);

    /// <summary>
    /// Deletes recipients from hard bounces list
    /// </summary>
    Task<bool> DeleteFromHardBouncesAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes recipients from hard bounces list (synchronous)
    /// </summary>
    bool DeleteFromHardBounces(string[]? ids = null, bool deleteAll = false, string? domainId = null);

    /// <summary>
    /// Gets spam complaint recipients
    /// </summary>
    Task<SuppressionsList> GetSpamComplaintsAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets spam complaint recipients (synchronous)
    /// </summary>
    SuppressionsList GetSpamComplaints(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Adds recipients to spam complaints list
    /// </summary>
    Task<bool> AddToSpamComplaintsAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds recipients to spam complaints list (synchronous)
    /// </summary>
    bool AddToSpamComplaints(string[] recipients, string? domainId = null);

    /// <summary>
    /// Deletes recipients from spam complaints list
    /// </summary>
    Task<bool> DeleteFromSpamComplaintsAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes recipients from spam complaints list (synchronous)
    /// </summary>
    bool DeleteFromSpamComplaints(string[]? ids = null, bool deleteAll = false, string? domainId = null);

    /// <summary>
    /// Gets unsubscribed recipients
    /// </summary>
    Task<SuppressionsList> GetUnsubscribesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets unsubscribed recipients (synchronous)
    /// </summary>
    SuppressionsList GetUnsubscribes(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Adds recipients to unsubscribes list
    /// </summary>
    Task<bool> AddToUnsubscribesAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds recipients to unsubscribes list (synchronous)
    /// </summary>
    bool AddToUnsubscribes(string[] recipients, string? domainId = null);

    /// <summary>
    /// Deletes recipients from unsubscribes list
    /// </summary>
    Task<bool> DeleteFromUnsubscribesAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes recipients from unsubscribes list (synchronous)
    /// </summary>
    bool DeleteFromUnsubscribes(string[]? ids = null, bool deleteAll = false, string? domainId = null);
}
