using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// The role service.
    /// </summary>
    public class RoleService : BaseEntityService<Role>, IRoleService
    {
        private readonly AuthDBContext authDBContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="authDBContext">The auth d b context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public RoleService(AuthDBContext authDBContext, ILoggerFactory loggerFactory) 
            : base(authDBContext)
        {
            this.authDBContext = authDBContext;
            this.logger = loggerFactory.CreateLogger<RoleService>();
        }

        /// <summary>
        /// Gets the role ids by names.
        /// </summary>
        /// <param name="roleNames">The role names.</param>
        /// <returns>A list of int.</returns>
        public List<int> GetRoleIdsByNames(List<string> roleNames)
        {
            return Where(r => roleNames.Contains(r.Name)).Select(r => r.ID).ToList();
        }

        /// <summary>
        /// Gets the role names by ids.
        /// </summary>
        /// <param name="roleIDs">The role i ds.</param>
        /// <returns>A list of Roles.</returns>
        public IEnumerable<Role> GetRoleNamesByIds(IEnumerable<int> roleIDs)
        {
            return Where(r => roleIDs.Contains(r.ID)).Distinct().ToList();
        }
    }
}
