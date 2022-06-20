using System.ComponentModel.DataAnnotations;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The feature.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        public Feature()
        {
            FeatureAttributes = new List<FeatureAttribute>();
        }

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
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the ParentID of the feature.
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// Gets or Sets Feature associations
        /// </summary>
        public ICollection<FeatureAssociation> FeatureAssociations { get; set; }

        /// <summary>
        /// Gets or Sets FeatureAttributes Navigation Property.
        /// </summary>
        public ICollection<FeatureAttribute> FeatureAttributes { get; set; }
    }
}
