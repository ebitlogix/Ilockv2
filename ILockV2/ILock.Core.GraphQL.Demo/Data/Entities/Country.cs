using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The country entity
    /// </summary>
    [Table(nameof(Country))]
    public class Country
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
