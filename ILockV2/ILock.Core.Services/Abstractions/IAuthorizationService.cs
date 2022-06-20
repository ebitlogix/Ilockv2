using ILock.Core.Data.Entities;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Authorization Service Interface
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Authorizes the.
        /// </summary>
        /// <param name="policyName">The policy name.</param>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
        Task<bool> Authorize(string policyName, User user);
    }
}
