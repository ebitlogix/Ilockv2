// <copyright file="FeatureQueryExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Data.Constants;
using ILock.Core.GraphQL.Extensions.Constants;
using ILock.Core.GraphQL.Extensions.Queries;
using ILock.Core.GraphQL.Extensions.Types;

namespace ILock.Core.GraphQL.Extensions.QueryExtensions
{
    /// <summary>
    /// The feature query extension.
    /// </summary>
    public class FeatureQueryExtension : ObjectTypeExtension<Query>, ILockAuthQueryType
    {
        private readonly string permissionName;
        private readonly List<string> accessLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureQueryExtension"/> class.
        /// </summary>
        public FeatureQueryExtension()
        {
            this.permissionName = nameof(EntityName.Feature);
            this.accessLevels = new List<string>
                {
                    nameof(AccessLevel.Full), nameof(AccessLevel.Read)
                };
        }

        /// <summary>
        /// Configures the.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(nameof(FeatureQueryResolver.GetFeatures))
                .ResolveWith<FeatureQueryResolver>(_ => _.GetFeatures(default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());

            descriptor.Field(nameof(FeatureQueryResolver.GetFeaturesByExternalId))
                .Argument("id", a => a.Type<NonNullType<IntType>>())
                .Argument("type", a => a.Type<NonNullType<StringType>>())
            .ResolveWith<FeatureQueryResolver>(_ => _.GetFeaturesByExternalId(default, default, default)).ConfigureAuthorization(this.permissionName, this.accessLevels.ToArray());
        }
    }
}
