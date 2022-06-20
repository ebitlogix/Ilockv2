using HotChocolate.Data;
using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.Queries
{
    /// <summary>
    /// The queries related to Roles module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class RoleQueryResolver : ILockQueryType
    {
        /// <summary>
        /// Gets the Roles.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetRoles")]
        public IQueryable<Data.Entities.Role> GetRoles([Service] AuthDBContext appDbContext)
        {
            return appDbContext.Roles;
        }

        /// <summary>
        /// Gets the Roles with id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <param name="type">type of external entity</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetRoleById")]
        public Data.Entities.Role GetRolesById([Service] AuthDBContext appDbContext, int id)
        {
            return appDbContext.Roles.FirstOrDefault(f => f.ID == id);
        }
    }
}
