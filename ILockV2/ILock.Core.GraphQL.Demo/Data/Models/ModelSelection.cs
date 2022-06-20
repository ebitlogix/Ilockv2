using ILock.Core.GraphQL.Demo.Data.Entities;

namespace ILock.Core.GraphQL.Demo.Data.Models
{
    /// <summary>
    /// The model selection.
    /// </summary>
    public class ModelSelection
    {
        /// <summary>
        /// Gets or sets the CountryID.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Gets or sets the RetailerID.
        /// </summary>
        public Retailer Retailer { get; set; }

        /// <summary>
        /// Gets or sets the ScenrioID.
        /// </summary>
        public Scenario Scenario { get; set; }
    }
}
