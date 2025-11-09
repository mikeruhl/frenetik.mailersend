#if NETSTANDARD2_0
// Polyfill for init-only setters in netstandard2.0
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved for compiler use
    /// </summary>
    internal static class IsExternalInit { }
}
#endif

namespace Frenetik.MailerSend.Models.Util
{
    /// <summary>
    /// Pagination parameters for list requests
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Gets or sets the page number
        /// </summary>
        public int Page { get; init; } = 1;

        /// <summary>
        /// Gets or sets the number of results per page
        /// </summary>
        public int Limit { get; init; } = 25;

        /// <summary>
        /// Validates the pagination parameters
        /// </summary>
        public void Validate()
        {
            if (Page < 1)
            {
                throw new ArgumentException("Page must be greater than or equal to 1", nameof(Page));
            }

            if (Limit < 1 || Limit > 100)
            {
                throw new ArgumentException("Limit must be between 1 and 100", nameof(Limit));
            }
        }
    }
}
