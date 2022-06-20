using ILock.Core.Data.Entities;

namespace ILock.Core.Data.Models
{
    /// <summary>
    /// User Model to contain tokens and extra information
    /// </summary>
    public class UserModel : User
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }
    }

}