// <copyright file="FeatureQueryResolver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Data;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.Queries
{
    /// <summary>
    /// The queries related to Features module.
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class FeatureQueryResolver : ILockQueryType
    {
        /// <summary>
        /// Gets the Features.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getFeatures")]
        public IQueryable<Data.Entities.Feature> GetFeatures([Service] AuthDBContext appDbContext)
        {
            return appDbContext.Features;
        }

        /// <summary>
        /// Gets the features associated with external id.
        /// </summary>
        /// <param name="appDbContext">The app db context.</param>
        /// <param name="id">The id.</param>
        /// <param name="type">type of external entity</param>
        /// <returns>An IQueryable.</returns>
        [UseFiltering]
        [UseSorting]
        [GraphQLName("getFeaturesByExternalId")]
        public IQueryable<Data.Entities.Feature> GetFeaturesByExternalId([Service] AuthDBContext appDbContext, int id, string type)
        {
            return appDbContext.FeatureAssociations.Where(f => f.ExternalID == id && f.Key == type).Select(f => f.Feature);
        }
    }
}
