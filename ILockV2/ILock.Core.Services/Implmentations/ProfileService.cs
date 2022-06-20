using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements ProfileService Interface
    /// </summary>
    public class ProfileService : BaseEntityService<User>, IProfileService
    {
        private readonly AuthDBContext authDBContext;
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService"/> class.
        /// </summary>
        /// <param name="authDbContext">The auth db context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ProfileService(AuthDBContext authDbContext, ILoggerFactory loggerFactory) : base(authDbContext)
        {
            this.logger = loggerFactory.CreateLogger<ProfileService>();
            this.authDBContext = authDbContext;
        }

        /// <summary>
        /// Updates the.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
