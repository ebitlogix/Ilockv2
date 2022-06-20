using ILock.Core.Data.Entities;

namespace ILock.Core.Data
{
    /// <summary>
    /// The authentication result.
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResult"/> class.
        /// </summary>
        /// <param name="success">If true, success.</param>
        /// <param name="error">The error.</param>
        /// <param name="token">The token.</param>
        /// <param name="user">The user.</param>
        public AuthenticationResult(bool success, string error, AuthToken token, User user)
        {
            Success = success;
            Error = error;
            Token = token;
            User = user;
        }
        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public AuthToken Token { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }
    }
}
