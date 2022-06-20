using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.Queries
{



    /// <summary>
    /// The country query resolver.
    /// </summary>

    [ExtendObjectType(typeof(Query))]
    public class CountryQueryResolver
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getCountries")]
        public IQueryable<Data.Entities.Country> GetCountries([Service] DemoDbContext appDbContext)
        {
            return appDbContext.Countries;
        }

    }
}
