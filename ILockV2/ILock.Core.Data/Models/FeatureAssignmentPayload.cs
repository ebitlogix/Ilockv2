namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The feature assignment payload.
    /// </summary>
    public class FeatureAssignmentPayload : ILockSchemaType
    {
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
        public int[] FeatureIDs { get; set; }

    }
}
