using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The scenario.
    /// </summary>
    [Table(nameof(Scenario))]
    public class Scenario
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets RetailerID
        /// </summary>
        public int RetailerID { get; set; }

        /// <summary>
        /// Gets or sets the retailer.
        /// </summary>
        public Retailer Retailer { get; set; }
    }
}
