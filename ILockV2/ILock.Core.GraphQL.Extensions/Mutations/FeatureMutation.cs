using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.Mutations
{
    /// <summary>
    /// The Feature module mutations.
    /// </summary>
    [ExtendObjectType(nameof(Mutation))]
    public class FeatureMutation : ILockMutationType
    {
        /// <summary>
        /// Assigns the features with external id.
        /// </summary>
        /// <param name="featureAssignmentPayload">The feature assignment payload.</param>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<ILock.Core.Data.Models.FeatureAssignmentPayload> AssignFeaturesWithExternalId(ILock.Core.Data.Models.FeatureAssignmentPayload featureAssignmentPayload, [Service] AuthDBContext context, CancellationToken cancellationToken)
        {
            var availableFeatures = context.FeatureAssociations.Where(fa => fa.ExternalID == featureAssignmentPayload.ExternalID).ToList();
            context.FeatureAssociations.RemoveRange(availableFeatures);

            foreach (var featureId in featureAssignmentPayload.FeatureIDs)
            {
                context.FeatureAssociations.Add(new Data.Entities.FeatureAssociation
                {
                    Key = featureAssignmentPayload.Key,
                    Value = featureAssignmentPayload.Value,
                    ExternalID = featureAssignmentPayload.ExternalID,
                    FeatureID = featureId,
                    AccessType = null
                });
            }

            await context.SaveChangesAsync();
            return featureAssignmentPayload;
        }
    }
}
