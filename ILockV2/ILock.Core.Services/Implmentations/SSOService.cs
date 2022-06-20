using ILock.Core.Data;
using ILock.Core.Data.Models;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// The s s o service.
    /// </summary>
    public class SSOService : BaseEntityService<SSOAuthToken>, ISSOService
    {
        private readonly AuthDBContext authDBContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSOService"/> class.
        /// </summary>
        /// <param name="authDBContext">The auth d b context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public SSOService(AuthDBContext authDBContext, ILoggerFactory loggerFactory)
            : base(authDBContext)
        {
            this.authDBContext = authDBContext;
            this.logger = loggerFactory.CreateLogger<SSOService>();
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A SSOAuthToken.</returns>
        public SSOAuthToken GetById(string id)
        {
            return this.authDBContext.SSOAuthTokens.FirstOrDefault(t => t.ID == id);
        }
    }
}
