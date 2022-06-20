using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.Queries
{
    /// <summary>
    /// The retailer query resolver.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class RetailerQueryResolver
    {
        /// <summary>
        /// Gets the Retailer.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getRetailers")]
        public IQueryable<Data.Entities.Retailer> GetRetailers([Service] DemoDbContext appDbContext)
        {
            return appDbContext.Retailers;
        }

        /// <summary>
        /// Gets the Retailer by ID.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getRetailersById")]
        public IQueryable<Data.Entities.Retailer> GetRetailersByID(int countryId, [Service] DemoDbContext appDbContext)
        {
            return appDbContext.Retailers.Where(r => r.CountryID == countryId);
        }
    }
}
