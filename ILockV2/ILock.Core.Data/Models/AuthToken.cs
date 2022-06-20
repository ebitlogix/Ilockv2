using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.Data
{
    /// <summary>
    /// The token type.
    /// </summary>
    public enum TokenType
    {
        Bearer,
        Refresh,
        ID
    }

    /// <summary>
    /// The auth token.
    /// </summary>
    public class AuthToken
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [NotMapped]
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public TokenType Type { get; set; }
        /// <summary>
        /// Gets or sets the issued at.
        /// </summary>
        public DateTime IssuedAt { get; set; }
        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        public DateTime ExpiryDate { get; set; }
    }
}
