using System.ComponentModel.DataAnnotations.Schema;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The feature requirement.
    /// </summary>
    [Table(nameof(FeatureRequirement))]
    public class FeatureRequirement
    {
        /// <summary>
        /// Gets or sets the i d.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the requirement i d.
        /// </summary>
        public int RequirementID { get; set; }
        /// <summary>
        /// Gets or sets the feature i d.
        /// </summary>
        public int FeatureID { get; set; }
        /// <summary>
        /// Gets or sets the policy i d.
        /// </summary>
        public int PolicyID { get; set; }
        /// <summary>
        /// Gets or sets the requirement.
        /// </summary>
        public Requirement Requirement { get; set; }
        /// <summary>
        /// Gets or sets the feature.
        /// </summary>
        public Feature Feature { get; set; }
        /// <summary>
        /// Gets or sets the policy.
        /// </summary>
        public Policy Policy { get; set; }
    }
}
