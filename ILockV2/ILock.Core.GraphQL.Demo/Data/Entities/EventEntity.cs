using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The event entity.
    /// </summary>
    [Table(nameof(EventEntity))]
    public class EventEntity
    {
        // Event ID
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public int ID { get; set; }

        // 20210101
        /// <summary>
        /// Gets or sets the to.
        /// </summary>
        public string To { get; set; }
        // 20210101
        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
