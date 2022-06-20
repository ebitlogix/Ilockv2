namespace ILock.Core
{
    /// <summary>
    /// The token settings.
    /// </summary>
    public class TokenSettings
    {
        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }
    }
}
