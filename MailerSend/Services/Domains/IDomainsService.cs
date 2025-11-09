using MailerSend.Models.Domains;
using MailerSend.Models.Util;

namespace MailerSend.Services.Domains;

/// <summary>
/// Service for managing domains
/// </summary>
public interface IDomainsService
{
    /// <summary>
    /// Gets a paginated list of domains
    /// </summary>
    /// <param name="pagination">Pagination parameters (page and limit)</param>
    /// <param name="verified">Filter by verification status (null = all, true = verified only, false = unverified only)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<DomainsList> GetDomainsAsync(
        PaginationParameters? pagination = null,
        bool? verified = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of domains (synchronous)
    /// </summary>
    DomainsList GetDomains(
        PaginationParameters? pagination = null,
        bool? verified = null);

    /// <summary>
    /// Gets a single domain by ID
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Domain> GetDomainAsync(string domainId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single domain by ID (synchronous)
    /// </summary>
    Domain GetDomain(string domainId);

    /// <summary>
    /// Adds a new domain
    /// </summary>
    /// <param name="name">Domain name</param>
    /// <param name="returnPathSubdomain">Optional return path subdomain</param>
    /// <param name="customTrackingSubdomain">Optional custom tracking subdomain</param>
    /// <param name="inboundRoutingSubdomain">Optional inbound routing subdomain</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Domain> AddDomainAsync(
        string name,
        string? returnPathSubdomain = null,
        string? customTrackingSubdomain = null,
        string? inboundRoutingSubdomain = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new domain (synchronous)
    /// </summary>
    Domain AddDomain(
        string name,
        string? returnPathSubdomain = null,
        string? customTrackingSubdomain = null,
        string? inboundRoutingSubdomain = null);

    /// <summary>
    /// Updates domain settings
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="settings">Domain settings to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Domain> UpdateDomainSettingsAsync(
        string domainId,
        DomainSettings settings,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates domain settings (synchronous)
    /// </summary>
    Domain UpdateDomainSettings(string domainId, DomainSettings settings);

    /// <summary>
    /// Deletes a domain
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<bool> DeleteDomainAsync(string domainId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a domain (synchronous)
    /// </summary>
    bool DeleteDomain(string domainId);

    /// <summary>
    /// Gets recipients for a domain
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="pagination">Pagination parameters (page and limit)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<ApiRecipientsList> GetDomainRecipientsAsync(
        string domainId,
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets recipients for a domain (synchronous)
    /// </summary>
    ApiRecipientsList GetDomainRecipients(
        string domainId,
        PaginationParameters? pagination = null);

    /// <summary>
    /// Gets DNS records for a domain
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<DomainDnsRecords> GetDomainDnsRecordsAsync(
        string domainId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets DNS records for a domain (synchronous)
    /// </summary>
    DomainDnsRecords GetDomainDnsRecords(string domainId);

    /// <summary>
    /// Verifies a domain
    /// </summary>
    /// <param name="domainId">Domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<DomainVerificationStatus> VerifyDomainAsync(
        string domainId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies a domain (synchronous)
    /// </summary>
    DomainVerificationStatus VerifyDomain(string domainId);
}
