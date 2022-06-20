namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The SSO auth token.
    /// </summary>
    public class SSOAuthToken
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the user i d.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets the session index.
        /// </summary>
        public string SessionIndex { get; set; }
    }
}