using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The retailer.
    /// </summary>
    [Table(nameof(Retailer))]
    public class Retailer
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country i d.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public Country Country { get; set; }
    }
}
