using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements IRegistrationService Interface
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        public RegistrationService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<RegistrationService>();
        }
        /// <summary>
        /// Registers the.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An int.</returns>
        public int Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
