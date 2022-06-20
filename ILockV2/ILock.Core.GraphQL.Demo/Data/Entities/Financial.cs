using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.GraphQL.Demo.Data.Entities
{
    /// <summary>
    /// The financial entity.
    /// </summary>
    [Table(nameof(Financial))]
    public class Financial
    {
        /// <summary>
        /// Gets or sets Financial ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets List Price
        /// </summary>
        public double Lsp { get; set; }

        /// <summary>
        /// Gets or sets Discount1
        /// </summary>
        public double? Discount1 { get; set; }

        /// <summary>
        /// Gets or sets Product SKU
        /// </summary>
        public string ProductSKU { get; set; }
    }
}
