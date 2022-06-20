using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Extensions.Queries;

namespace ILock.Core.GraphQL.Demo.Queries
{
    /// <summary>
    /// The scenario query resolver.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class ScenarioQueryResolver
    {

        /// <summary>
        /// Gets the Retailer.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getScenarios")]
        public IQueryable<Data.Entities.Scenario> GetScenarios([Service] DemoDbContext appDbContext)
        {
            return appDbContext.Scenarios;
        }
    }
}
