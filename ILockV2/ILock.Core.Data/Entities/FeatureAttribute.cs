using System.ComponentModel.DataAnnotations;
namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The feature attribute.
    /// </summary>
    public class FeatureAttribute
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
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the feature.
        /// </summary>
        public Feature Feature { get; set; }
    }
}
