using System.Text.Json;
using System.Text.Json.Serialization;
using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Recipients;
using MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.Recipients;

/// <summary>
/// Service implementation for managing recipient suppression lists
/// </summary>
public class RecipientsService : ServiceBase, IRecipientsService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecipientsService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public RecipientsService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    #region Blocklist

    /// <summary>
    /// Gets a paginated list of blocklisted recipients
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of blocklisted recipients</returns>
    public async Task<SuppressionsList> GetBlocklistAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await GetSuppressionsAsync("blocklist", pagination, domainId, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of blocklisted recipients (synchronous)
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <returns>A list of blocklisted recipients</returns>
    public SuppressionsList GetBlocklist(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetBlocklistAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Adds recipients to the blocklist
    /// </summary>
    /// <param name="recipients">Email addresses to blocklist</param>
    /// <param name="domainId">Domain ID to associate with the blocklist entries</param>
    /// <param name="patterns">Email patterns to blocklist</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> AddToBlocklistAsync(
        string[] recipients,
        string? domainId = null,
        string[]? patterns = null,
        CancellationToken cancellationToken = default)
    {
        var request = new SuppressionAddRequest
        {
            DomainId = domainId,
            Recipients = recipients,
            Patterns = patterns
        };

        return await AddSuppressionsAsync("blocklist", request, cancellationToken);
    }

    /// <summary>
    /// Adds recipients to the blocklist (synchronous)
    /// </summary>
    /// <param name="recipients">Email addresses to blocklist</param>
    /// <param name="domainId">Domain ID to associate with the blocklist entries</param>
    /// <param name="patterns">Email patterns to blocklist</param>
    /// <returns>True if successful</returns>
    public bool AddToBlocklist(string[] recipients, string? domainId = null, string[]? patterns = null)
    {
        return AddToBlocklistAsync(recipients, domainId, patterns).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Removes recipients from the blocklist
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> DeleteFromBlocklistAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await DeleteSuppressionsAsync("blocklist", ids, deleteAll, domainId, cancellationToken);
    }

    /// <summary>
    /// Removes recipients from the blocklist (synchronous)
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <returns>True if successful</returns>
    public bool DeleteFromBlocklist(string[]? ids = null, bool deleteAll = false, string? domainId = null)
    {
        return DeleteFromBlocklistAsync(ids, deleteAll, domainId).GetAwaiter().GetResult();
    }

    #endregion

    #region Hard Bounces

    /// <summary>
    /// Gets a paginated list of hard bounced recipients
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of hard bounced recipients</returns>
    public async Task<SuppressionsList> GetHardBouncesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await GetSuppressionsAsync("hard-bounces", pagination, domainId, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of hard bounced recipients (synchronous)
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <returns>A list of hard bounced recipients</returns>
    public SuppressionsList GetHardBounces(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetHardBouncesAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Adds recipients to the hard bounces list
    /// </summary>
    /// <param name="recipients">Email addresses to add to hard bounces</param>
    /// <param name="domainId">Domain ID to associate with the hard bounce entries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> AddToHardBouncesAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        var request = new SuppressionAddRequest
        {
            DomainId = domainId,
            Recipients = recipients
        };

        return await AddSuppressionsAsync("hard-bounces", request, cancellationToken);
    }

    /// <summary>
    /// Adds recipients to the hard bounces list (synchronous)
    /// </summary>
    /// <param name="recipients">Email addresses to add to hard bounces</param>
    /// <param name="domainId">Domain ID to associate with the hard bounce entries</param>
    /// <returns>True if successful</returns>
    public bool AddToHardBounces(string[] recipients, string? domainId = null)
    {
        return AddToHardBouncesAsync(recipients, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Removes recipients from the hard bounces list
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> DeleteFromHardBouncesAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await DeleteSuppressionsAsync("hard-bounces", ids, deleteAll, domainId, cancellationToken);
    }

    /// <summary>
    /// Removes recipients from the hard bounces list (synchronous)
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <returns>True if successful</returns>
    public bool DeleteFromHardBounces(string[]? ids = null, bool deleteAll = false, string? domainId = null)
    {
        return DeleteFromHardBouncesAsync(ids, deleteAll, domainId).GetAwaiter().GetResult();
    }

    #endregion

    #region Spam Complaints

    /// <summary>
    /// Gets a paginated list of spam complaint recipients
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of spam complaint recipients</returns>
    public async Task<SuppressionsList> GetSpamComplaintsAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await GetSuppressionsAsync("spam-complaints", pagination, domainId, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of spam complaint recipients (synchronous)
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <returns>A list of spam complaint recipients</returns>
    public SuppressionsList GetSpamComplaints(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetSpamComplaintsAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Adds recipients to the spam complaints list
    /// </summary>
    /// <param name="recipients">Email addresses to add to spam complaints</param>
    /// <param name="domainId">Domain ID to associate with the spam complaint entries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> AddToSpamComplaintsAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        var request = new SuppressionAddRequest
        {
            DomainId = domainId,
            Recipients = recipients
        };

        return await AddSuppressionsAsync("spam-complaints", request, cancellationToken);
    }

    /// <summary>
    /// Adds recipients to the spam complaints list (synchronous)
    /// </summary>
    /// <param name="recipients">Email addresses to add to spam complaints</param>
    /// <param name="domainId">Domain ID to associate with the spam complaint entries</param>
    /// <returns>True if successful</returns>
    public bool AddToSpamComplaints(string[] recipients, string? domainId = null)
    {
        return AddToSpamComplaintsAsync(recipients, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Removes recipients from the spam complaints list
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> DeleteFromSpamComplaintsAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await DeleteSuppressionsAsync("spam-complaints", ids, deleteAll, domainId, cancellationToken);
    }

    /// <summary>
    /// Removes recipients from the spam complaints list (synchronous)
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <returns>True if successful</returns>
    public bool DeleteFromSpamComplaints(string[]? ids = null, bool deleteAll = false, string? domainId = null)
    {
        return DeleteFromSpamComplaintsAsync(ids, deleteAll, domainId).GetAwaiter().GetResult();
    }

    #endregion

    #region Unsubscribes

    /// <summary>
    /// Gets a paginated list of unsubscribed recipients
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of unsubscribed recipients</returns>
    public async Task<SuppressionsList> GetUnsubscribesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await GetSuppressionsAsync("unsubscribes", pagination, domainId, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of unsubscribed recipients (synchronous)
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="domainId">Filter by domain ID</param>
    /// <returns>A list of unsubscribed recipients</returns>
    public SuppressionsList GetUnsubscribes(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetUnsubscribesAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Adds recipients to the unsubscribes list
    /// </summary>
    /// <param name="recipients">Email addresses to add to unsubscribes</param>
    /// <param name="domainId">Domain ID to associate with the unsubscribe entries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> AddToUnsubscribesAsync(
        string[] recipients,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        var request = new SuppressionAddRequest
        {
            DomainId = domainId,
            Recipients = recipients
        };

        return await AddSuppressionsAsync("unsubscribes", request, cancellationToken);
    }

    /// <summary>
    /// Adds recipients to the unsubscribes list (synchronous)
    /// </summary>
    /// <param name="recipients">Email addresses to add to unsubscribes</param>
    /// <param name="domainId">Domain ID to associate with the unsubscribe entries</param>
    /// <returns>True if successful</returns>
    public bool AddToUnsubscribes(string[] recipients, string? domainId = null)
    {
        return AddToUnsubscribesAsync(recipients, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Removes recipients from the unsubscribes list
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    public async Task<bool> DeleteFromUnsubscribesAsync(
        string[]? ids = null,
        bool deleteAll = false,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        return await DeleteSuppressionsAsync("unsubscribes", ids, deleteAll, domainId, cancellationToken);
    }

    /// <summary>
    /// Removes recipients from the unsubscribes list (synchronous)
    /// </summary>
    /// <param name="ids">Suppression IDs to remove</param>
    /// <param name="deleteAll">Delete all entries for the domain</param>
    /// <param name="domainId">Domain ID to filter deletions</param>
    /// <returns>True if successful</returns>
    public bool DeleteFromUnsubscribes(string[]? ids = null, bool deleteAll = false, string? domainId = null)
    {
        return DeleteFromUnsubscribesAsync(ids, deleteAll, domainId).GetAwaiter().GetResult();
    }

    #endregion

    #region Helper Methods

    private async Task<SuppressionsList> GetSuppressionsAsync(
        string listType,
        PaginationParameters? pagination,
        string? domainId,
        CancellationToken cancellationToken)
    {
        pagination ??= new PaginationParameters();
        var mailerSendHttpClient = CreateHttpClient();

        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        if (!string.IsNullOrEmpty(domainId))
        {
            queryParams.Add($"domain_id={domainId}");
        }

        var endpoint = $"suppressions/{listType}?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<SuppressionsList>(endpoint, cancellationToken);
    }

    private async Task<bool> AddSuppressionsAsync(
        string listType,
        SuppressionAddRequest request,
        CancellationToken cancellationToken)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = $"suppressions/{listType}";
        var response = await mailerSendHttpClient.PostRequestAsync<MailerSendResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return IsSuccessStatusCode(response.ResponseStatusCode);
    }

    private async Task<bool> DeleteSuppressionsAsync(
        string listType,
        string[]? ids,
        bool deleteAll,
        string? domainId,
        CancellationToken cancellationToken)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new SuppressionDeleteRequest
        {
            DomainId = domainId,
            Ids = ids ?? Array.Empty<string>(),
            All = deleteAll ? true : null
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = $"suppressions/{listType}";
        var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return IsSuccessStatusCode(response.ResponseStatusCode);
    }

    #endregion
}
