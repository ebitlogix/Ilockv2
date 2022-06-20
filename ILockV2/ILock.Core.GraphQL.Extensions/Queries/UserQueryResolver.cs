using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.GraphQL.Extensions.Queries
{
    /// <summary>
    /// The queries related to Users module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class UserQueryResolver : ILockQueryType
    {
        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetUsers")]
        public IQueryable<Data.Entities.User> GetUsers([Service] AuthDBContext appDbContext)
        {
            return appDbContext.Users;
        }

        /// <summary>
        /// Gets the User associated with id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetUserById")]
        public Data.Entities.User GetUserById([Service] AuthDBContext appDbContext, int id)
        {
            return appDbContext.Users.FirstOrDefault(u => u.ID == id);
        }

        /// <summary>
        /// Gets the users roles.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetUserRoles")]
        public IQueryable<Data.Entities.Role> GetUsersRoles([Service] AuthDBContext appDbContext, int userId)
        {
            return appDbContext.Users.Include(u => u.Roles).FirstOrDefault(u => u.ID == userId).Roles.AsQueryable();
        }
    }
}
