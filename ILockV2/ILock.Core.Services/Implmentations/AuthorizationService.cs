using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements AuthorizationService Interface
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        public AuthorizationService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<AuthorizationService>();
        }

        /// <summary>
        /// Authorizes the.
        /// </summary>
        /// <param name="policyName">The policy name.</param>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
        public Task<bool> Authorize(string policyName, User user)
        {
            return Task.FromResult(true);
        }
    }
}
