using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements IFeatureService Interface
    /// </summary>
    public class FeatureService : BaseEntityService<Feature>, IFeatureService
    {
        private readonly AuthDBContext authDBContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureService"/> class.
        /// Initializes a new instance of feature service
        /// </summary>
        /// <param name="dbContext"></param>
        public FeatureService(AuthDBContext dbContext, ILoggerFactory loggerFactory) : base(dbContext)
        {
            this.authDBContext = dbContext;
            this.logger = loggerFactory.CreateLogger<FeatureService>();
        }

        public IEnumerable<Feature> GetFeaturesByExternalId(int id, string type)
        {
            return authDBContext.FeatureAssociations.Where(f => f.ExternalID == id && f.Key == type).Select(f => f.Feature);
        }

        public async Task<Data.Models.FeatureAssignmentPayload> AssignFeaturesWithExternalId(Data.Models.FeatureAssignmentPayload featureAssignmentPayload)
        {
            var availableFeaturesAssociations = authDBContext.FeatureAssociations.Where(fa => fa.ExternalID == featureAssignmentPayload.ExternalID).ToList();
            authDBContext.FeatureAssociations.RemoveRange(availableFeaturesAssociations);

            foreach (var featureId in featureAssignmentPayload.FeatureIDs)
            {
                authDBContext.FeatureAssociations.Add(new Data.Entities.FeatureAssociation
                {
                    Key = featureAssignmentPayload.Key,
                    Value = featureAssignmentPayload.Value,
                    ExternalID = featureAssignmentPayload.ExternalID,
                    FeatureID = featureId,
                    AccessType = null
                });
            }

            await authDBContext.SaveChangesAsync();
            return featureAssignmentPayload;

        }
    }
}
