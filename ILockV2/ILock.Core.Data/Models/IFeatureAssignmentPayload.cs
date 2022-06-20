namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The feature assignment payload.
    /// </summary>
    public interface IFeatureAssignmentPayload
    {
        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        int ExternalID { get; set; }
        /// <summary>
        /// Gets or sets the feature ids.
        /// </summary>
        int[] FeatureIDs { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        string Value { get; set; }
    }
}