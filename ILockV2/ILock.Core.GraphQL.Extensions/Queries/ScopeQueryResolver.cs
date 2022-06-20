using HotChocolate.Data;
using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;
using Microsoft.EntityFrameworkCore;

namespace ILock.Core.GraphQL.Extensions.Queries
{
    /// <summary>
    /// The queries related to Features module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class ScopeQueryResolver : ILockQueryType
    {
        /// <summary>
        /// Gets the scopes.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetScopes")]
        public IQueryable<Data.Entities.Scope> GetScopes([Service] AuthDBContext appDbContext)
        {
            return appDbContext.Scopes;
        }

        /// <summary>
        /// Gets the scope by id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Data.Entities.Scope.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("GetScopeById")]
        public Data.Entities.Scope GetScopeById([Service] AuthDBContext appDbContext, int id)
        {
            return appDbContext.Scopes.Include(s => s.ParentScope).FirstOrDefault(f => f.ID == id);
        }
    }
}
