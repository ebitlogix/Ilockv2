using System.ComponentModel.DataAnnotations;

namespace ILock.Core.Data.Entities
{
    /// <summary>
    /// The feature association with any entity.
    /// </summary>
    public class FeatureAssociation
    {
        /// <summary>
        /// Gets or Sets ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or Sets Association Name/ Key e.g Country
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or Sets Value e.g. Russia
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets External Entity ID e.g. Country ID or any entity which is associated with this feature
        /// </summary>
        public int ExternalID { get; set; }

        /// <summary>
        /// Gets or Sets FeatureID
        /// </summary>
        public int FeatureID { get; set; }

        /// <summary>
        /// Gets or
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// Gets or Sets Access type for this association with the feature. e.g. Full, Write, None. Value is filled using ILock.Core.Data.Entities.AccessLevel enum
        /// </summary>
        public string AccessType { get; set; }
    }
}
