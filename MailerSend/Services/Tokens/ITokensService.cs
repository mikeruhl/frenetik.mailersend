using MailerSend.Models.Tokens;
using MailerSend.Models.Util;

namespace MailerSend.Services.Tokens;

/// <summary>
/// Service for managing API tokens
/// </summary>
public interface ITokensService
{
    /// <summary>
    /// Gets a paginated list of tokens
    /// </summary>
    /// <param name="pagination">Pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<TokensList> GetTokensAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of tokens (synchronous)
    /// </summary>
    TokensList GetTokens(PaginationParameters? pagination = null);

    /// <summary>
    /// Gets a single token by ID
    /// </summary>
    /// <param name="tokenId">Token ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Token> GetTokenAsync(string tokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single token by ID (synchronous)
    /// </summary>
    Token GetToken(string tokenId);

    /// <summary>
    /// Creates a new API token
    /// </summary>
    /// <param name="name">Token name</param>
    /// <param name="domainId">Domain ID</param>
    /// <param name="scopes">Token scopes</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<TokenAdd> CreateTokenAsync(
        string name,
        string domainId,
        string[] scopes,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new API token (synchronous)
    /// </summary>
    TokenAdd CreateToken(string name, string domainId, string[] scopes);

    /// <summary>
    /// Updates a token's status
    /// </summary>
    /// <param name="tokenId">Token ID</param>
    /// <param name="status">New status (pause/unpause)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Token> UpdateTokenStatusAsync(
        string tokenId,
        string status,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a token's status (synchronous)
    /// </summary>
    Token UpdateTokenStatus(string tokenId, string status);

    /// <summary>
    /// Updates a token's name
    /// </summary>
    /// <param name="tokenId">Token ID</param>
    /// <param name="name">New name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Token> UpdateTokenNameAsync(
        string tokenId,
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a token's name (synchronous)
    /// </summary>
    Token UpdateTokenName(string tokenId, string name);

    /// <summary>
    /// Deletes a token
    /// </summary>
    /// <param name="tokenId">Token ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<bool> DeleteTokenAsync(string tokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a token (synchronous)
    /// </summary>
    bool DeleteToken(string tokenId);
}
