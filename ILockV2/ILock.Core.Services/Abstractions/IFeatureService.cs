using ILock.Core.Data.Entities;
using ILock.Core.Data.Models;

namespace ILock.Core.Services.Abstractions
{
    /// <summary>
    /// Feature Service Interface
    /// </summary>
    public interface IFeatureService : IBaseEntityService<Feature>
    {
        /// <summary>
        /// Assigns the features with external id.
        /// </summary>
        /// <param name="featureAssignmentPayload">The feature assignment payload.</param>
        /// <returns>A Task.</returns>
        Task<FeatureAssignmentPayload> AssignFeaturesWithExternalId(FeatureAssignmentPayload featureAssignmentPayload);

        /// <summary>
        /// Gets the features by external id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="type">The type.</param>
        /// <returns>A list of Features.</returns>
        IEnumerable<Feature> GetFeaturesByExternalId(int id, string type);
    }
}
