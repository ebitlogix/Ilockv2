using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.Queries
{
    /// <summary>
    /// The forecast related queries.
    /// </summary>
    public class FinancialQueryResolver
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getFinancials")]
        public IQueryable<Data.Entities.Financial> GetFinancials([Service] DemoDbContext appDbContext)
        {
            return appDbContext.Financials;
        }

        /// <summary>
        /// Gets the event by id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Financial.</returns>
        [GraphQLName("getFinancialById")]
        public Data.Entities.Financial GetFinancialById([Service] DemoDbContext appDbContext, int id)
        {
            return appDbContext.Financials.FirstOrDefault(x => x.ID == id);
        }
    }
}
