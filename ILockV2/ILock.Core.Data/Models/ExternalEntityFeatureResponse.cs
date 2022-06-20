using ILock.Core.Data.Entities;

namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The external entity feature response.
    /// </summary>
    public class ExternalEntityFeatureResponse
    {
        /// <summary>
        /// Gets or sets the feature.
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// Gets or sets AccessType
        /// </summary>
        public string AccessType { get; set; }
    }
}
